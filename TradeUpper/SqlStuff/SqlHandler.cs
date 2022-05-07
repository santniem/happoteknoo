using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TradeUpper.SqlStuff
{
    public class SqlHandler
    {
        private SqlConnection connection;
        public SqlHandler(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }


        public DataSet SelectRows(string queryString)
        {
            connection.Open();

            var dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(queryString, connection);
            adapter.Fill(dataset);
            connection.Close();

            return dataset;

        }
    }
}
