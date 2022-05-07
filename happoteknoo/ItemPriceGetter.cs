using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NäätäETL;
using NäätäETL.DataFlows;
using NäätäETL.ETLPipeLine;
using NäätäETL.Scripter;
using SteamApiJuttu;
using SteamApiJuttu.Models;
using SteamApiJuttu.Models.Steam;

namespace happoteknoo
{
    public class ItemPriceGetter
    {

        private ActualFlow dataflow;
        public ItemPriceGetter(string connectionString,bool Truncate)
        {
            var typeToUse = typeof(CleanPriceModel);
            dataflow = new ActualFlow(typeToUse,connectionString,Truncate);
        }

        public async Task GetAndInsertCases(string cookie)
        {
            PriceHistoryClient client = new PriceHistoryClient(cookie);
            
            foreach (string casename in Cases.CaseNames)
            {
                var priceList = await client.GetItemPriceHistory(casename);
                dataflow.postData(priceList);
                Console.WriteLine($"data {casename} added to transaction");
            }

            await dataflow.CleanUpFlows();
        }
        
        public async Task GetAndInsertItems(string cookie,string[] items)
        {
            PriceHistoryClient client = new PriceHistoryClient(cookie);
            
            for (var index = 0; index < items.Length; index++)
            {
                string itemName = items[index];
                var priceList = await client.GetItemPriceHistory(itemName);
                if (priceList != null)
                {
                    dataflow.postData(priceList);
                    Console.WriteLine($"[{DateTime.Now}] [{index} / {items.Length}] Price history retrieved for {itemName}");
                }
                else
                {
                    Console.WriteLine($"[{DateTime.Now}] [{index} / {items.Length}] No prices for {itemName}");
                }
            }

            await dataflow.CleanUpFlows();
        }
    }
}