using System;
using System.Data;
public class AnalysisDataMapper
{
	public static DataSet mapValue(DataSet ds, object juttu)
	{
		AnalysisData.ValueMapper(ds, juttu);
		return ds;
	}
	public static DataSet makeDataset()
	{
		var ds = new DataSet("AnalysisData");
		// AnalysisData TABLE DEFINITION
		DataTable dt_AnalysisData = new DataTable("AnalysisData");
		dt_AnalysisData.Columns.Add("usedCollections", typeof(String));
		dt_AnalysisData.Columns.Add("NumOutputs", typeof(Int32));
		dt_AnalysisData.Columns.Add("statTrak", typeof(Boolean));
		dt_AnalysisData.Columns.Add("rarity", typeof(String));
		dt_AnalysisData.Columns.Add("inputFloat", typeof(Single));
		dt_AnalysisData.Columns.Add("ExpectedValue", typeof(Double));
		dt_AnalysisData.Columns.Add("Profit", typeof(Double));
		dt_AnalysisData.Columns.Add("ProfitWithFree", typeof(Double));
		dt_AnalysisData.Columns.Add("InputPrice", typeof(Double));
		dt_AnalysisData.Columns.Add("Profitability", typeof(Double));
		dt_AnalysisData.Columns.Add("AnalysisDataGUID", typeof(System.Guid));

		ds.Tables.Add(dt_AnalysisData);
		// InputSkins TABLE DEFINITION
		DataTable dt_InputSkins = new DataTable("InputSkins");
		dt_InputSkins.Columns.Add("CostPerFloat", typeof(Double));
		dt_InputSkins.Columns.Add("CostPerFloatRevers", typeof(Double));
		dt_InputSkins.Columns.Add("statTrak", typeof(Boolean));
		dt_InputSkins.Columns.Add("collectionName", typeof(String));
		dt_InputSkins.Columns.Add("rarity", typeof(String));
		dt_InputSkins.Columns.Add("ExteriorStr", typeof(String));
		dt_InputSkins.Columns.Add("Price", typeof(Double));
		dt_InputSkins.Columns.Add("name", typeof(String));
		dt_InputSkins.Columns.Add("FloatTarget", typeof(Single));
		dt_InputSkins.Columns.Add("SkinFloat", typeof(Single));
		dt_InputSkins.Columns.Add("MinFloat", typeof(Single));
		dt_InputSkins.Columns.Add("MaxFloat", typeof(Single));
		dt_InputSkins.Columns.Add("TradeupAvgFloat", typeof(Single));
		dt_InputSkins.Columns.Add("AnalysisDataGUID", typeof(System.Guid));
		dt_InputSkins.Columns.Add("InputSkinsGUID", typeof(System.Guid));

		ds.Tables.Add(dt_InputSkins);
		// output TABLE DEFINITION
		DataTable dt_output = new DataTable("output");
		dt_output.Columns.Add("Name", typeof(String));
		dt_output.Columns.Add("SkinFloat", typeof(Single));
		dt_output.Columns.Add("Price", typeof(Double));
		dt_output.Columns.Add("luck", typeof(Double));
		dt_output.Columns.Add("AnalysisDataGUID", typeof(System.Guid));
		dt_output.Columns.Add("outputGUID", typeof(System.Guid));

		ds.Tables.Add(dt_output);
		return ds;
	}

	public static class AnalysisData
	{
		public static void ValueMapper(DataSet ds, dynamic a)
		{
			var row = ds.Tables["AnalysisData"].NewRow();
			var guid = Guid.NewGuid();
			if (a == null) { return; }
			row["usedCollections"] = a.usedCollections ?? DBNull.Value;
			row["NumOutputs"] = a.NumOutputs ?? DBNull.Value;
			row["statTrak"] = a.statTrak ?? DBNull.Value;
			row["rarity"] = a.rarity ?? DBNull.Value;
			row["inputFloat"] = a.inputFloat ?? DBNull.Value;
			row["ExpectedValue"] = a.ExpectedValue ?? DBNull.Value;
			row["Profit"] = a.Profit ?? DBNull.Value;
			row["ProfitWithFree"] = a.ProfitWithFree ?? DBNull.Value;
			row["InputPrice"] = a.InputPrice ?? DBNull.Value;
			row["Profitability"] = a.Profitability ?? DBNull.Value;
			row["AnalysisDataGUID"] = guid;
			ds.Tables["AnalysisData"].Rows.Add(row);
			Skin.ValueMapper(ds, a.InputSkins, guid);
			ResultSkin.ValueMapper(ds, a.output, guid);
		}

	}

	public static class Skin
	{
		public static void ValueMapper(DataSet ds, dynamic iterable, Guid parentGuid)
		{
			if (iterable == null) { return; }
			foreach (var item in iterable)
			{
				var row = ds.Tables["InputSkins"].NewRow();
				var guid = Guid.NewGuid();
				row["InputSkinsGUID"] = guid;
				row["AnalysisDataGUID"] = parentGuid;
				row["CostPerFloat"] = item.CostPerFloat ?? DBNull.Value;
				row["CostPerFloatRevers"] = item.CostPerFloatRevers ?? DBNull.Value;
				row["statTrak"] = item.statTrak ?? DBNull.Value;
				row["collectionName"] = item.collectionName ?? DBNull.Value;
				row["rarity"] = item.rarity ?? DBNull.Value;
				row["ExteriorStr"] = item.ExteriorStr ?? DBNull.Value;
				row["Price"] = item.Price ?? DBNull.Value;
				row["name"] = item.name ?? DBNull.Value;
				row["FloatTarget"] = item.FloatTarget ?? DBNull.Value;
				row["SkinFloat"] = item.SkinFloat ?? DBNull.Value;
				row["MinFloat"] = item.MinFloat ?? DBNull.Value;
				row["MaxFloat"] = item.MaxFloat ?? DBNull.Value;
				row["TradeupAvgFloat"] = item.TradeupAvgFloat ?? DBNull.Value;
				ds.Tables["InputSkins"].Rows.Add(row);
			}
		}

	}

	public static class ResultSkin
	{
		public static void ValueMapper(DataSet ds, dynamic iterable, Guid parentGuid)
		{
			if (iterable == null) { return; }
			foreach (var item in iterable)
			{
				var row = ds.Tables["output"].NewRow();
				var guid = Guid.NewGuid();
				row["outputGUID"] = guid;
				row["AnalysisDataGUID"] = parentGuid;
				row["Name"] = item.Name ?? DBNull.Value;
				row["SkinFloat"] = item.SkinFloat ?? DBNull.Value;
				row["Price"] = item.Price ?? DBNull.Value;
				row["luck"] = item.luck ?? DBNull.Value;
				ds.Tables["output"].Rows.Add(row);
			}
		}

	}

}
