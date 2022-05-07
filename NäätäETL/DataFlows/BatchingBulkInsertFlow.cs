using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Gridsum.DataflowEx;

namespace NäätäETL.DataFlows
{
    public class BatchingBulkInsertFlow : Dataflow<DataTable>
    {
        private readonly DataTable auxTable;
        private readonly int batchTreshhold = 10;
        protected readonly ActionBlock<DataRow[]> m_actionBlock;
        protected readonly TransformManyBlock<IEnumerable<DataRow>, DataRow> m_arrayFlattener;
        protected readonly BatchBlock<DataRow> m_batchBlock;
        private readonly int m_bulkSize;

        protected readonly TransformBlock<DataTable, IEnumerable<DataRow>> m_rowCloner;


        private int batchCount;

        private string ConnectionString;
        protected SqlConnection m_longConnection;

        protected Timer m_timer;
        protected SqlTransaction m_transaction;

        public BatchingBulkInsertFlow(DataTable schemaTable, string connectionString, int bulkSize) : base(
            DataflowOptions.Default)
        {
            m_bulkSize = bulkSize;
            m_longConnection = new SqlConnection(connectionString);
            m_longConnection.Open();
            m_transaction = m_longConnection.BeginTransaction();

            auxTable = schemaTable.Clone();
            ConnectionString = connectionString;


            m_rowCloner = new TransformBlock<DataTable, IEnumerable<DataRow>>(dt => batchRows(dt));
            m_arrayFlattener = new TransformManyBlock<IEnumerable<DataRow>, DataRow>(x => x);
            m_batchBlock = new BatchBlock<DataRow>(m_bulkSize);

            m_actionBlock = new ActionBlock<DataRow[]>(
                async array =>
                {
                    try
                    {
                        await DumpToDBAsync(array);
                    }
                    catch (Exception e)
                    {
                        CleanUp(e);
                        throw;
                    }
                }
            );

            m_rowCloner.LinkTo(m_arrayFlattener, new DataflowLinkOptions {PropagateCompletion = true});
            m_arrayFlattener.LinkTo(m_batchBlock, new DataflowLinkOptions {PropagateCompletion = true});
            m_batchBlock.LinkTo(m_actionBlock, new DataflowLinkOptions {PropagateCompletion = true});

            RegisterChild(m_batchBlock);
            RegisterChild(m_actionBlock);

            m_timer = new Timer(
                state => { TriggerBatch(); }, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        }

        private string targetTable => auxTable.TableName;

        public override ITargetBlock<DataTable> InputBlock => m_rowCloner;


        private IEnumerable<DataRow> batchRows(DataTable from)
        {
            foreach (DataRow row in from.Rows)
            {
                var desRow = auxTable.NewRow();
                desRow.ItemArray = row.ItemArray.Clone() as object[];
                yield return desRow;
            }
        }

        protected async Task DumpToDBAsync(DataRow[] array)
        {
            try
            {
                using (var bulkCopy = new SqlBulkCopy(m_longConnection, SqlBulkCopyOptions.TableLock, m_transaction))
                {
                    foreach (DataColumn map in auxTable.Columns)
                    {
                        var mapping = new SqlBulkCopyColumnMapping(map.ColumnName, map.ColumnName);
                        bulkCopy.ColumnMappings.Add(mapping);
                    }

                    bulkCopy.DestinationTableName = auxTable.TableName;
                    bulkCopy.BulkCopyTimeout = (int) TimeSpan.FromMinutes(5).TotalMilliseconds;
                    bulkCopy.BatchSize = m_bulkSize;


                    // Write from the source to the destination.
                    await bulkCopy.WriteToServerAsync(array);

                    batchCount++;
                    if (batchCount > batchTreshhold)
                    {
                        await m_transaction.CommitAsync();
                        m_transaction = m_longConnection.BeginTransaction();
                        batchCount = 0;
                        auxTable.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                if (e is NullReferenceException)
                {
                    //m_logger.Error($"{this.FullName} NullReferenceException occurred in bulk insertion for type {typeof(T).Name}. This is probably caused by forgetting assigning value to a [NoNullCheck] attribute when constructing your object.");
                }

                //As this is an unrecoverable exception, rethrow it
                throw;
            }
        }

        protected override void CleanUp(Exception dataflowException)
        {
            if (dataflowException != null)
                // m_logger.ErrorFormat("{0} Rolling back all changes...", this.FullName, dataflowException);
                m_transaction.Rollback();
            //  m_logger.InfoFormat("{0} Changes successfully rolled back", this.FullName);
            else
                // m_logger.InfoFormat("{0} bulk insertions are done. Committing transaction...", this.FullName);
                m_transaction.Commit();
            // m_logger.DebugFormat("{0} Transaction successfully committed.", this.FullName);

            m_longConnection.Close();
        }

        public void TriggerBatch()
        {
            m_batchBlock.TriggerBatch();
        }
    }
}