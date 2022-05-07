using System;
using Flurl;
using Flurl.Util;
using Flurl.Http;
using SteamApiJuttu.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamApiJuttu.Models.Steam;

namespace SteamApiJuttu
{
    public class PriceHistoryClient
    {
        private readonly RequestRateHelper throttler;
        private  string cookie { get; set; }
        public PriceHistoryClient(string _cookie)
        {
            cookie = _cookie;
            throttler = new RequestRateHelper(1, new TimeSpan(0,0,0,1));
        }

        public async Task<CleanPriceModel> GetItemPriceHistory(string itemName)
        {
            throttler.SleepAsNeeded();
            try
            {
                var request = "https://steamcommunity.com/market/pricehistory/"
                    .SetQueryParam("currenry", 3) //EURO
                    .SetQueryParam("appid", 730) //CSGO
                    .SetQueryParam("market_hash_name", itemName)
                    .WithHeader("Cookie", cookie);

                var response = await request.GetAsync();
                if (response.StatusCode == 200)
                {
                    var json = await  response.GetStringAsync();
                    var priceHistory = PriceHistory.FromJsonToCleaned(json, itemName);

                    return priceHistory;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }


        }
    }
}
