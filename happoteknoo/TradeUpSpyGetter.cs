using NäätäETL;
using SteamApiJuttu;
using SteamApiJuttu.Models.TradeUpSpy;
using System;
using System.Threading.Tasks;

namespace happoteknoo
{
    internal class TradeUpSpyGetter
    {


        private ActualFlow dataflow;
        private string tradeUpCookie;
        public TradeUpSpyGetter(string connectionString, string _tradeUpCookie, bool truncate)
        {
            var typeToUse = typeof(Skin);
            dataflow = new ActualFlow(typeToUse, connectionString, truncate);
            tradeUpCookie = _tradeUpCookie;
        }
        public async Task GetAndInsertCollectionSkins()
        {
            TradeUpSpyClient tradeUpSpyClient = new TradeUpSpyClient(tradeUpCookie);

            var collections = await tradeUpSpyClient.GetCollections();
            
            foreach (var collection in collections)
            {
                Console.WriteLine($"Collection {collection.Name}");

                var collectionSkins = await tradeUpSpyClient.GetSkinList(collection.Idc);
                foreach (var skinSummary in collectionSkins)
                {
                    var skin = await tradeUpSpyClient.GetSkin(skinSummary.Ids);
                    dataflow.postData(skin);
                }

            }
            await dataflow.CleanUpFlows();
        }

    }
}
