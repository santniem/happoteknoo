using System.Data;
using System.Threading.Tasks.Dataflow;
using Gridsum.DataflowEx;

namespace NäätäETL.DataFlows
{
    public class ObjectFlow : Dataflow<DataTable>
    {
        private readonly Dataflow<DataTable> bulk;
        private Dataflow<DataTable, DataTable> createTableflow;

        public ObjectFlow(string ConnectionString, DataTable dataTable,bool Truncate) : base(DataflowOptions.Default)
        {
            createTableflow = new TableCreateFlow(ConnectionString,Truncate);
            bulk = new BatchingBulkInsertFlow(dataTable, ConnectionString, 16384);

            createTableflow.LinkTo(bulk);

            RegisterChild(createTableflow);
            RegisterChild(bulk);
        }

        public override ITargetBlock<DataTable> InputBlock => createTableflow.InputBlock;
    }
}