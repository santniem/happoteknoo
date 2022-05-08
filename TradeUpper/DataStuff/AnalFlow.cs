using NäätäETL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeUpper.DataStuff
{
    internal class AnalFlow
    {


        private ActualFlow dataflow;
        public AnalFlow(string connectionString, bool Truncate)
        {
            var typeToUse = typeof(AnalysisData);
            dataflow = new ActualFlow(typeToUse, connectionString, Truncate);
        }
        public Task postData(IEnumerable<AnalysisData> data)
        {

            foreach (var dat in data)
            {
                return Task.Run(() =>
                {
                    dataflow.postData(dat);
                });
            }
            return Task.CompletedTask;
        }

        public async Task clean()
        {
            await dataflow.CleanUpFlows();
        }
    }
}
