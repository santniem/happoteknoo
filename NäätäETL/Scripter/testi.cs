using System;
using System.Data;

namespace NäätäETL.Scripter
{
    public class invoiceMapper
    {
        public static DataSet testi(DataSet ds, object juttu)
        {
            invoice.ValueMapper(ds, juttu);
            return ds;
        }

        public static DataSet makeDataset()
        {
            var ds = new DataSet("invoice");
            // invoice TABLE DEFINITION
            var dt_invoice = new DataTable("invoice");
            dt_invoice.Columns.Add("testi", typeof(int));
            dt_invoice.Columns.Add("customer_Id", typeof(int));
            dt_invoice.Columns.Add("customer_name", typeof(string));
            dt_invoice.Columns.Add("customer_homeAddress_street", typeof(string));
            dt_invoice.Columns.Add("customer_homeAddress_city", typeof(string));
            dt_invoice.Columns.Add("customer_homeAddress_coord_x", typeof(int));
            dt_invoice.Columns.Add("customer_homeAddress_coord_y", typeof(int));
            dt_invoice.Columns.Add("customer_deliveryAddress_street", typeof(string));
            dt_invoice.Columns.Add("customer_deliveryAddress_city", typeof(string));
            dt_invoice.Columns.Add("customer_deliveryAddress_coord_x", typeof(int));
            dt_invoice.Columns.Add("customer_deliveryAddress_coord_y", typeof(int));

            ds.Tables.Add(dt_invoice);
            // CustomerItemList TABLE DEFINITION
            var dt_CustomerItemList = new DataTable("CustomerItemList");
            dt_CustomerItemList.Columns.Add("numero", typeof(int));

            ds.Tables.Add(dt_CustomerItemList);
            // addresses TABLE DEFINITION
            var dt_addresses = new DataTable("addresses");
            dt_addresses.Columns.Add("street", typeof(string));
            dt_addresses.Columns.Add("city", typeof(string));
            dt_addresses.Columns.Add("coord_x", typeof(int));
            dt_addresses.Columns.Add("coord_y", typeof(int));

            ds.Tables.Add(dt_addresses);
            // invoiceRows TABLE DEFINITION
            var dt_invoiceRows = new DataTable("invoiceRows");
            dt_invoiceRows.Columns.Add("summa", typeof(int));
            dt_invoiceRows.Columns.Add("rivi", typeof(int));
            dt_invoiceRows.Columns.Add("tuote", typeof(string));

            ds.Tables.Add(dt_invoiceRows);
            return ds;
        }

        public static class invoice
        {
            public static void ValueMapper(DataSet ds, dynamic a)
            {
                var row = ds.Tables["invoice"].NewRow();
                row["testi"] = a.testi ?? DBNull.Value;
                row["customer_Id"] = a.customer?.Id ?? DBNull.Value;
                row["customer_name"] = a.customer?.name ?? DBNull.Value;
                row["customer_homeAddress_street"] = a.customer?.homeAddress?.street ?? DBNull.Value;
                row["customer_homeAddress_city"] = a.customer?.homeAddress?.city ?? DBNull.Value;
                row["customer_homeAddress_coord_x"] = a.customer?.homeAddress?.coord?.x ?? DBNull.Value;
                row["customer_homeAddress_coord_y"] = a.customer?.homeAddress?.coord?.y ?? DBNull.Value;
                row["customer_deliveryAddress_street"] = a.customer?.deliveryAddress?.street ?? DBNull.Value;
                row["customer_deliveryAddress_city"] = a.customer?.deliveryAddress?.city ?? DBNull.Value;
                row["customer_deliveryAddress_coord_x"] = a.customer?.deliveryAddress?.coord?.x ?? DBNull.Value;
                row["customer_deliveryAddress_coord_y"] = a.customer?.deliveryAddress?.coord?.y ?? DBNull.Value;
                ds.Tables["invoice"].Rows.Add(row);
                invoiceRow.ValueMapper(ds, a.invoiceRows);
                CustomersListItem.ValueMapper(ds, a.customer.CustomerItemList);
            }
        }

        public static class CustomersListItem
        {
            public static void ValueMapper(DataSet ds, dynamic iterable)
            {
                foreach (var item in iterable)
                {
                    var row = ds.Tables["CustomerItemList"].NewRow();
                    row["numero"] = item.numero ?? DBNull.Value;
                    ds.Tables["CustomerItemList"].Rows.Add(row);
                    Address.ValueMapper(ds, item.addresses);
                }
            }
        }

        public static class Address
        {
            public static void ValueMapper(DataSet ds, dynamic iterable)
            {
                foreach (var item in iterable)
                {
                    var row = ds.Tables["addresses"].NewRow();
                    row["street"] = item.street ?? DBNull.Value;
                    row["city"] = item.city ?? DBNull.Value;
                    row["coord_x"] = item.x ?? DBNull.Value;
                    row["coord_y"] = item.y ?? DBNull.Value;
                    ds.Tables["addresses"].Rows.Add(row);
                }
            }
        }

        public static class invoiceRow
        {
            public static void ValueMapper(DataSet ds, dynamic iterable)
            {
                foreach (var item in iterable)
                {
                    var row = ds.Tables["invoiceRows"].NewRow();
                    row["summa"] = item.summa ?? DBNull.Value;
                    row["rivi"] = item.rivi ?? DBNull.Value;
                    row["tuote"] = item.tuote ?? DBNull.Value;
                    ds.Tables["invoiceRows"].Rows.Add(row);
                }
            }
        }
    }
}