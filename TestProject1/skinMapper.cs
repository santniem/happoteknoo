using System;
using System.Data;
public class CsMoneyItemModelMapper {
    public static DataSet mapValue(DataSet ds,object juttu)
                                {
                                    CsMoneyItemModel.ValueMapper(ds, juttu);
                                    return ds;
                                }
	 public static DataSet makeDataset(){
var ds = new DataSet("CsMoneyItemModel");
		 // CsMoneyItemModel TABLE DEFINITION
		 DataTable dt_CsMoneyItemModel = new DataTable("CsMoneyItemModel");
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_Id",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_HashName",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_Name",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_Description",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_Slug",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("PageProps_Entry_Type",typeof(String));
		 dt_CsMoneyItemModel.Columns.Add("CsMoneyItemModelGUID",typeof(System.Guid));

		 ds.Tables.Add(dt_CsMoneyItemModel);
		 // Items TABLE DEFINITION
		 DataTable dt_Items = new DataTable("Items");
		 dt_Items.Columns.Add("Id",typeof(String));
		 dt_Items.Columns.Add("HashName",typeof(String));
		 dt_Items.Columns.Add("Name",typeof(String));
		 dt_Items.Columns.Add("Rarity",typeof(String));
		 dt_Items.Columns.Add("ReleaseDate",typeof(Int64));
		 dt_Items.Columns.Add("Slug",typeof(String));
		 dt_Items.Columns.Add("Subtitle",typeof(String));
		 dt_Items.Columns.Add("Title",typeof(String));
		 dt_Items.Columns.Add("Type",typeof(String));
		 dt_Items.Columns.Add("Image_AbsolutePath",typeof(String));
		 dt_Items.Columns.Add("Image_AbsoluteUri",typeof(String));
		 dt_Items.Columns.Add("Image_LocalPath",typeof(String));
		 dt_Items.Columns.Add("Image_Authority",typeof(String));
		 dt_Items.Columns.Add("Image_HostNameType",typeof(UriHostNameType));
		 dt_Items.Columns.Add("Image_IsDefaultPort",typeof(Boolean));
		 dt_Items.Columns.Add("Image_IsFile",typeof(Boolean));
		 dt_Items.Columns.Add("Image_IsLoopback",typeof(Boolean));
		 dt_Items.Columns.Add("Image_PathAndQuery",typeof(String));
		 dt_Items.Columns.Add("Image_IsUnc",typeof(Boolean));
		 dt_Items.Columns.Add("Image_Host",typeof(String));
		 dt_Items.Columns.Add("Image_Port",typeof(Int32));
		 dt_Items.Columns.Add("Image_Query",typeof(String));
		 dt_Items.Columns.Add("Image_Fragment",typeof(String));
		 dt_Items.Columns.Add("Image_Scheme",typeof(String));
		 dt_Items.Columns.Add("Image_OriginalString",typeof(String));
		 dt_Items.Columns.Add("Image_DnsSafeHost",typeof(String));
		 dt_Items.Columns.Add("Image_IdnHost",typeof(String));
		 dt_Items.Columns.Add("Image_IsAbsoluteUri",typeof(Boolean));
		 dt_Items.Columns.Add("Image_UserEscaped",typeof(Boolean));
		 dt_Items.Columns.Add("Image_UserInfo",typeof(String));
		 dt_Items.Columns.Add("Texts_AppearanceHistory",typeof(String));
		 dt_Items.Columns.Add("Price_Common_Min",typeof(Double));
		 dt_Items.Columns.Add("Price_Common_Max",typeof(Double));
		 dt_Items.Columns.Add("Price_Special_Min",typeof(Double));
		 dt_Items.Columns.Add("Price_Special_Max",typeof(Double));
		 dt_Items.Columns.Add("EntryGUID",typeof(System.Guid));
		 dt_Items.Columns.Add("ItemsGUID",typeof(System.Guid));

		 ds.Tables.Add(dt_Items);
		 // Collection TABLE DEFINITION
		 DataTable dt_Collection = new DataTable("Collection");
		 dt_Collection.Columns.Add("Id",typeof(String));
		 dt_Collection.Columns.Add("Name",typeof(String));
		 dt_Collection.Columns.Add("Slug",typeof(String));
		 dt_Collection.Columns.Add("ItemsGUID",typeof(System.Guid));
		 dt_Collection.Columns.Add("CollectionGUID",typeof(System.Guid));

		 ds.Tables.Add(dt_Collection);
		 // Containers TABLE DEFINITION
		 DataTable dt_Containers = new DataTable("Containers");
		 dt_Containers.Columns.Add("Id",typeof(String));
		 dt_Containers.Columns.Add("Name",typeof(String));
		 dt_Containers.Columns.Add("Slug",typeof(String));
		 dt_Containers.Columns.Add("ItemsGUID",typeof(System.Guid));
		 dt_Containers.Columns.Add("ContainersGUID",typeof(System.Guid));

		 ds.Tables.Add(dt_Containers);
	 return ds;
	}

	public static class CsMoneyItemModel {
		public static void ValueMapper(DataSet ds,dynamic a) {
			var row = ds.Tables["CsMoneyItemModel"].NewRow();
			 var guid = Guid.NewGuid();
				 if(a == null){return;}
			 row["PageProps_Entry_Id"] = a.PageProps?.Entry?.Id ?? DBNull.Value;
			 row["PageProps_Entry_HashName"] = a.PageProps?.Entry?.HashName ?? DBNull.Value;
			 row["PageProps_Entry_Name"] = a.PageProps?.Entry?.Name ?? DBNull.Value;
			 row["PageProps_Entry_Description"] = a.PageProps?.Entry?.Description ?? DBNull.Value;
			 row["PageProps_Entry_Slug"] = a.PageProps?.Entry?.Slug ?? DBNull.Value;
			 row["PageProps_Entry_Type"] = a.PageProps?.Entry?.Type ?? DBNull.Value;
			 row["CsMoneyItemModelGUID"] = guid;
			ds.Tables["CsMoneyItemModel"].Rows.Add(row);
			 Item.ValueMapper(ds, a.PageProps.Entry.Items,guid );
		}

	}

	public static class Item {
		public static void ValueMapper(DataSet ds, dynamic iterable, Guid parentGuid) {
				 if(iterable == null){return;}
			 foreach (var item in iterable){
				 var row = ds.Tables["Items"].NewRow();
				 var guid = Guid.NewGuid();
					 row["ItemsGUID"] = guid;
					 row["EntryGUID"] = parentGuid;
					 row["Id"] = item.Id ?? DBNull.Value;
					 row["HashName"] = item.HashName ?? DBNull.Value;
					 row["Name"] = item.Name ?? DBNull.Value;
					 row["Rarity"] = item.Rarity ?? DBNull.Value;
					 row["ReleaseDate"] = item.ReleaseDate ?? DBNull.Value;
					 row["Slug"] = item.Slug ?? DBNull.Value;
					 row["Subtitle"] = item.Subtitle ?? DBNull.Value;
					 row["Title"] = item.Title ?? DBNull.Value;
					 row["Type"] = item.Type ?? DBNull.Value;
					 row["Image_AbsolutePath"] = item.Image?.AbsolutePath ?? DBNull.Value;
					 row["Image_AbsoluteUri"] = item.Image?.AbsoluteUri ?? DBNull.Value;
					 row["Image_LocalPath"] = item.Image?.LocalPath ?? DBNull.Value;
					 row["Image_Authority"] = item.Image?.Authority ?? DBNull.Value;
					 row["Image_HostNameType"] = item.Image?.HostNameType ?? DBNull.Value;
					 row["Image_IsDefaultPort"] = item.Image?.IsDefaultPort ?? DBNull.Value;
					 row["Image_IsFile"] = item.Image?.IsFile ?? DBNull.Value;
					 row["Image_IsLoopback"] = item.Image?.IsLoopback ?? DBNull.Value;
					 row["Image_PathAndQuery"] = item.Image?.PathAndQuery ?? DBNull.Value;
					 row["Image_IsUnc"] = item.Image?.IsUnc ?? DBNull.Value;
					 row["Image_Host"] = item.Image?.Host ?? DBNull.Value;
					 row["Image_Port"] = item.Image?.Port ?? DBNull.Value;
					 row["Image_Query"] = item.Image?.Query ?? DBNull.Value;
					 row["Image_Fragment"] = item.Image?.Fragment ?? DBNull.Value;
					 row["Image_Scheme"] = item.Image?.Scheme ?? DBNull.Value;
					 row["Image_OriginalString"] = item.Image?.OriginalString ?? DBNull.Value;
					 row["Image_DnsSafeHost"] = item.Image?.DnsSafeHost ?? DBNull.Value;
					 row["Image_IdnHost"] = item.Image?.IdnHost ?? DBNull.Value;
					 row["Image_IsAbsoluteUri"] = item.Image?.IsAbsoluteUri ?? DBNull.Value;
					 row["Image_UserEscaped"] = item.Image?.UserEscaped ?? DBNull.Value;
					 row["Image_UserInfo"] = item.Image?.UserInfo ?? DBNull.Value;
					 row["Texts_AppearanceHistory"] = item.Texts?.AppearanceHistory ?? DBNull.Value;
					 row["Price_Common_Min"] = item.Price?.Common?.Min ?? DBNull.Value;
					 row["Price_Common_Max"] = item.Price?.Common?.Max ?? DBNull.Value;
					 row["Price_Special_Min"] = item.Price?.Special?.Min ?? DBNull.Value;
					 row["Price_Special_Max"] = item.Price?.Special?.Max ?? DBNull.Value;
					 ds.Tables["Items"].Rows.Add(row);
			 Collection.ValueMapper(ds, item.Collection, guid );
			 Collection.ValueMapper(ds, item.Containers, guid );
			}
		}

	}

	public static class Collection {
		public static void ValueMapper(DataSet ds, dynamic iterable, Guid parentGuid) {
				 if(iterable == null){return;}
			 foreach (var item in iterable){
				 var row = ds.Tables["Collection"].NewRow();
				 var guid = Guid.NewGuid();
					 row["CollectionGUID"] = guid;
					 row["ItemsGUID"] = parentGuid;
					 row["Id"] = item.Id ?? DBNull.Value;
					 row["Name"] = item.Name ?? DBNull.Value;
					 row["Slug"] = item.Slug ?? DBNull.Value;
					 ds.Tables["Collection"].Rows.Add(row);
			}
		}

	}


}
