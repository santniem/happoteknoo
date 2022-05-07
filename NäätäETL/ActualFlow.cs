using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NäätäETL.DataFlows;
using NäätäETL.ETLPipeLine;
using NäätäETL.Scripter;

namespace NäätäETL
{
    public class ActualFlow
    {
        private DataMapperInvoker dataMapper;
        private Dictionary<string, ObjectFlow> flowCache;

        public ActualFlow(Type typeToUse,string connectionString,bool Truncate)
        {
            dataMapper = DataSource.purkkaa(typeToUse);
            flowCache = new Dictionary<string, ObjectFlow>();
            ConfigureFlows(connectionString,Truncate);
        }
        
        public void postData(object stuff)
        {
            var ds = dataMapper.getData(stuff);
            foreach (DataTable item in ds.Tables)
            {
                flowCache[item.TableName].InputBlock.Post(item);
            }
        }
        private void ConfigureFlows(string ConnectionString,bool Truncate)
        {
            Console.WriteLine("Scripts compiled");
            foreach (DataTable table in dataMapper.dataSet.Tables)
            {
                var objectFlow = new ObjectFlow(ConnectionString,table,Truncate);
                flowCache.Add(table.TableName, objectFlow);
            }
            Console.WriteLine("Dataflows configured");
        }
        
        public async Task CleanUpFlows()
        {
            foreach (ObjectFlow item in flowCache.Values)
            {
                Console.WriteLine($"finishing {item.Name}");
                await item.SignalAndWaitForCompletionAsync();
                Console.WriteLine($"finished {item.Name}");
            }
        }
    }
}