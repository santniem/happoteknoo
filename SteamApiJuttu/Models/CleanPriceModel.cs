using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamApiJuttu.Models.Steam
{
    public class CleanPriceModel
    {
        public string ItemName { get; set; }
        public List<ItemPrice> prices { get; set; }

    }
    public class ItemPrice
    {
        public ItemPrice(string? dateTime, double? price, long? itemSold)
        {
            var tmp = dateTime.Replace(": +0", ":00:00");
            this.dateTime = DateTime.Parse(tmp); 
            Price = price;
            ItemSold = itemSold;
        }


        public DateTime? dateTime { get; set; }
        public double? Price { get; set; }
        public long? ItemSold { get; set; }
    }
}
