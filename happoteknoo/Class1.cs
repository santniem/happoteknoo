using System;
using System.Data;
public class CleanPriceModelMapper
{
	public static DataSet mapValue(DataSet ds, object juttu)
	{
		CleanPriceModel.ValueMapper(ds, juttu);
		return ds;
	}
	public static DataSet makeDataset()
	{
		var ds = new DataSet("CleanPriceModel");
		// CleanPriceModel TABLE DEFINITION
		DataTable dt_CleanPriceModel = new DataTable("CleanPriceModel");
		dt_CleanPriceModel.Columns.Add("ItemName", typeof(String));

		ds.Tables.Add(dt_CleanPriceModel);
		// prices TABLE DEFINITION
		DataTable dt_prices = new DataTable("prices");
		dt_prices.Columns.Add("dateTime", typeof(String));
		dt_prices.Columns.Add("Price", typeof(Double));
		dt_prices.Columns.Add("ItemSold", typeof(Int64));

		ds.Tables.Add(dt_prices);
		return ds;
	}

	public static class CleanPriceModel
	{
		public static void ValueMapper(DataSet ds, dynamic a)
		{
			var row = ds.Tables["CleanPriceModel"].NewRow();
			row["ItemName"] = a.ItemName ?? DBNull.Value;
			ds.Tables["CleanPriceModel"].Rows.Add(row);
			ItemPrice.ValueMapper(ds, a.prices);
		}

	}

	public static class ItemPrice
	{
		public static void ValueMapper(DataSet ds, dynamic iterable)
		{
			foreach (var item in iterable)
			{
				var row = ds.Tables["prices"].NewRow();
				row["dateTime"] = item.dateTime ?? DBNull.Value;
				row["Price"] = item.Price ?? DBNull.Value;
				row["ItemSold"] = item.ItemSold ?? DBNull.Value;
				ds.Tables["prices"].Rows.Add(row);
			}
		}

	}

}
