using System;
using Flurl;
using Flurl.Util;
using Flurl.Http;
using SteamApiJuttu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamApiJuttu.Models.CsMoney;

namespace SteamApiJuttu
{
    public class ItemStatClient
    {
        private readonly RequestRateHelper throttler;
        private  string AppID { get; set; }
        public ItemStatClient(string appID)
        {
            throttler = new RequestRateHelper(1, new TimeSpan(0,0,0,1));
            AppID = appID;
        }

        public async Task<CsMoneyItemModel> getStats(string itemName)
        {
            
            throttler.SleepAsNeeded();

            var prices = await "https://wiki.cs.money/_next/data/"
                .AppendPathSegment(AppID)
                .AppendPathSegment("en")
                .AppendPathSegment("weapons")
                .AppendPathSegment(itemName + ".json")
                .GetStringAsync();
            var asd = CsMoneyItemModel.FromJson(prices);
            
            return asd;
        }
        public async Task<CsMoneyItemModel> getGloves(string itemName)
        {

            throttler.SleepAsNeeded();

            var prices = await "https://wiki.cs.money/_next/data/"
                .AppendPathSegment(AppID)
                .AppendPathSegment("en")
                .AppendPathSegment(itemName + ".json")
                .GetStringAsync();
            var asd = CsMoneyItemModel.FromJson(prices);

            return asd; 
        }
    }
}