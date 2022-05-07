
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using NäätäETL.DataFlows;
using NäätäETL.ETLPipeLine;
using SteamApiJuttu.Models;
using SteamApiJuttu;

namespace happoteknoo
{

    public class Program
    {

        static async Task Main(string[] args)
        {

            Console.WriteLine("Starting");

            var ConnectionString = "Data Source=Localhost;Initial Catalog=Stage_Steam;Trusted_Connection=True";
            string outoID = "G8-EkChAZKkifSTy63bN7";
            var steamToken = "steamLoginSecure=76561198035075716||7C242E68B2189076761DDCF5F980F69F84C79709";

            var tradeUpCookie = "JSESSIONID = DA02D0CAF22CC6EC4BD2A2FFEFDF5568; AWSELB = CDC323A11AE0DF00FE61C7A235364FB74A86C8C39FFE9FFB731B468B19F7E4F5935D1A830106F793CA2363CA4518603F6DE6F74AED8FFFA33845FB891700F64270A59DB118; AWSELBCORS = CDC323A11AE0DF00FE61C7A235364FB74A86C8C39FFE9FFB731B468B19F7E4F5935D1A830106F793CA2363CA4518603F6DE6F74AED8FFFA33845FB891700F64270A59DB118;";


            TradeUpSpyGetter getter = new TradeUpSpyGetter(ConnectionString, tradeUpCookie, true);
            await getter.GetAndInsertCollectionSkins();


            //ItemPriceGetter getter = new ItemPriceGetter(ConnectionString, false);

            //var items = File.ReadAllLines("C:/temp/steam/items.txt");
            //var testi = items.SelectMany(x => exteriors(x)).SelectMany(x => statTrak(x)).ToArray();
            //await getter.GetAndInsertItems(steamToken, testi);
            ////await getter.GetAndInsertCases(steamToken);

            ////SkinGetter skis = new SkinGetter(ConnectionString,outoID,false);
            ////await skis.GetAndInsertSkin("m4a4");
        }

        public static IEnumerable<string> statTrak(string itemName)
        {
            yield return $"{itemName}";
            if (itemName.StartsWith("★"))
            {
                yield return $"★ StatTrak™ {itemName.Replace("★ ","")}";
            }
            else
            {            
                yield return $"StatTrak™ {itemName}";
            }
        }

        public static IEnumerable<string> exteriors(string itemName)
        {
            yield return $"{itemName} (Factory New)";
            yield return $"{itemName} (Minimal Wear)";
            yield return $"{itemName} (Field-Tested)";
            yield return $"{itemName} (Well-Worn)";
            yield return $"{itemName} (Battle-Scarred)";
        }
    }
}
