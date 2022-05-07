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
using SteamApiJuttu.Models.CsMoney;
using SteamApiJuttu.Models.Steam;

namespace happoteknoo
{
    public class SkinGetter
    {

        private ActualFlow dataflow;
        private string AppID;
        public SkinGetter(string connectionString,string appID,bool truncate)
        {
            var typeToUse = typeof(CsMoneyItemModel);
            dataflow = new ActualFlow(typeToUse,connectionString, truncate);
            AppID = appID;
        }
        public async Task GetAndInsertGloves()
        {
            ItemStatClient client = new ItemStatClient(AppID);


            var weaponStats = await client.getGloves("gloves");
            dataflow.postData(weaponStats);
            Console.WriteLine($"data gloves added to transaction");
            
            await dataflow.CleanUpFlows();
        }
        public async Task GetAndInsertSkins()
        {
            ItemStatClient client = new ItemStatClient(AppID);
            
            foreach (string casename in Weapons.WeaponNames)
            {
                var weaponStats = await client.getStats(casename);
                dataflow.postData(weaponStats);
                Console.WriteLine($"data {casename} added to transaction");
            }
            foreach (string casename in Weapons.GetKnives)
            {
                var weaponStats = await client.getStats(casename);
                dataflow.postData(weaponStats);
                Console.WriteLine($"data {casename} added to transaction");
            }
            await dataflow.CleanUpFlows();
        }
        public async Task GetAndInsertSkin(string skin)
        {
            ItemStatClient client = new ItemStatClient(AppID);


            var weaponStats = await client.getStats(skin);
            dataflow.postData(weaponStats);
            Console.WriteLine($"data {skin} added to transaction");

            await dataflow.CleanUpFlows();
        }
    }
}