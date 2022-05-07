using System;
using System.IO;
using Xunit;
using Newtonsoft.Json;
using SteamApiJuttu.Models;
using SteamApiJuttu;
using System.Threading.Tasks;
using SteamApiJuttu.Models.CsMoney;
using SteamApiJuttu.Models.Steam;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var json = File.ReadAllText("C:/temp/steam/testi.json");
            var priceHistory = PriceHistory.FromJsonToCleaned(json,"temp") ;
            Assert.NotNull(priceHistory);
        }


        [Fact]
        public async Task Test2()
        {
            var steamToken = "steamLoginSecure=76561198035075716||C89C96D0515AE2F793CF6B7A54FD9CDD46572CB9";

            PriceHistoryClient client = new PriceHistoryClient(steamToken);
            var asd = await client.GetItemPriceHistory("AK-47 | Redline (Field-Tested)");
        }
        
        [Fact]
        public async Task Morat()
        {
            var steamToken = "steamLoginSecure=76561198035075716||C89C96D0515AE2F793CF6B7A54FD9CDD46572CB9";
            PriceHistoryClient client = new PriceHistoryClient(steamToken);
            var asd = await client.GetItemPriceHistory("★ Falchion Knife | Crimson Web (Field-Tested)");
        }


        [Fact]
        public void dateconvert()
        {
            var d = "Apr 18 2022 04:00:00";
            var a = DateTime.Now.ToString("MMM dd yyyy HH");
            var b = DateTime.Parse(d);
        }

        [Fact]
        public void Paskaa()
        {
            var json = File.ReadAllText("C:/temp/steam/ak-47.json");
            var priceHistory = CsMoneyItemModel.FromJson(json);

            var ds = CsMoneyItemModelMapper.makeDataset();
            var dat = CsMoneyItemModelMapper.mapValue(ds, priceHistory);
        }
        
        
        
        
        [Fact]
        public async Task aaaa()
        {
            var json = File.ReadAllText("C:/temp/steam/testi.json");
            var priceHistory = PriceHistory.FromJsonToCleaned(json, "temp");

            var ds = CleanPriceModelMapper.makeDataset();
            var dat = CleanPriceModelMapper.mapValue(ds, priceHistory);
            var g = Guid.NewGuid();
        }
    }
}
