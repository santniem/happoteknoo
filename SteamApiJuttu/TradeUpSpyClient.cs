using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using SteamApiJuttu.Models.TradeUpSpy;
namespace SteamApiJuttu
{
    public class TradeUpSpyClient
    {
        private readonly RequestRateHelper throttler;
        private string cookie { get; set; }
        public TradeUpSpyClient(string _cookie)
        {
            cookie = _cookie;
            throttler = new RequestRateHelper(10, new TimeSpan(0, 0, 0, 5));
        }


        public async Task<IEnumerable<Collection>> GetCollections()
        {
            throttler.SleepAsNeeded();
            var request = defaultHeaders("https://api.tradeupspy.com/api/collections/all");

            try
            {
                var a = await request.GetJsonAsync<List<Collection>>();
                return a.Where(x => x.Type == "case" || x.Type == "collection");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<SkinListSkin>> GetSkinList(long collectionID)
        {
            throttler.SleepAsNeeded();
            var request = defaultHeaders("https://api.tradeupspy.com/api/collections/full/")
                .AppendPathSegment(collectionID);

            try
            {
                var a = await request.GetJsonAsync<CollectionSkins>();
                return a.SkinList;
            }
            catch (Exception)
            {
                return Enumerable.Empty<SkinListSkin>();
            }
        }
        public async Task<Skin> GetSkin(long skinId)
        {
            throttler.SleepAsNeeded();
            var request = defaultHeaders("https://api.tradeupspy.com/api/skins/full/")
                .AppendPathSegment(skinId);

            try
            {
                var a = await request.GetJsonAsync<Skin>();
                return a;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private IFlurlRequest defaultHeaders(string url)
        {
            return url
                .WithHeader("Cookie", cookie)
                .WithHeader("Origin", "https://www.tradeupspy.com")
                .WithHeader("Referer", "https://www.tradeupspy.com/");
        }
    }
}
