using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;
using Gridsum.DataflowEx;

namespace NäätäETL.DataFlows
{
    public class TableCreateFlow : Dataflow<DataTable, DataTable>
    {
        private readonly TransformBlock<DataTable, DataTable> _action;

        private readonly TransformBlock<DataTable, DataTable> result = new(p => p);
        private bool Success;

        public TableCreateFlow(string connectionString,bool TruncateTable) : base(DataflowOptions.Default)
        {
            if (!TruncateTable)
            {
                Success = true;
            }
            _action = new TransformBlock<DataTable, DataTable>(dt =>
            {
                if (Success) return dt;
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var script = Helpers.CreateTABLE(dt.TableName, dt);
                // Poista olemassa oleva
                using var command = new SqlCommand(
                    @$"IF OBJECT_ID('{dt.TableName}', 'U') IS NOT NULL DROP TABLE [{dt.TableName}]; 
                    {script}",
                    connection);

                command.ExecuteNonQuery();
                connection.Close();
                Success = true;
                return dt;
            });
            _action.LinkTo(result, new DataflowLinkOptions {PropagateCompletion = true});

            RegisterChild(_action);
            RegisterChild(result);
        }

        public override ISourceBlock<DataTable> OutputBlock => result;

        public override ITargetBlock<DataTable> InputBlock => _action;
    }
}