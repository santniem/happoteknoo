using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUpper.Modes;
using TradeUpper.SqlStuff;

namespace TradeUpper.DataStuff
{
    public static class DataUpGetter
    {

        public static IEnumerable<Skin> GetInputData(string ConnectionString, Rarity rarity, IEnumerable<string> collections, bool _statTrak)
        {
            var statTrak = _statTrak ? 1 : 0;

            var minimums = getMinimumsByCollection(ConnectionString, rarity, collections, statTrak);
            foreach (DataRow row in minimums.Rows)
            {
                yield return new Skin(row[0]
                     , row[1]
                     , row[2]
                     , row[3]
                     , row[4]
                     , row[5]
                     , row[6]
                     , row[7]
                     , row[8]
                     );
            }
        }

        public static IEnumerable<string> getcollections(string ConnectionString,bool _statTrak)
        {
            var statTrak = _statTrak ? 1 : 0;
            var sql = new SqlHandler(ConnectionString);
            var query = $@"SELECT distinct collectionName FROM [DW].[dbo].[tradeup] 
where
statTrak = {statTrak}
AND CANBEINPUT = 1
AND collectionName NOT IN( 'Gods and Monsters Collection',
'St. Marc Collection',
'Rising Sun Collection',
'Norse Collection',
'Canals Collection')";
            var data = sql.SelectRows(query);
            foreach (DataRow item in data.Tables[0].Rows)
            {
                yield return item[0].ToString();
            }
        }

        public static IEnumerable<Skin> GetOutPutData(string ConnectionString, Rarity rarity, IEnumerable<string> collections, bool _statTrak)
        {
            var statTrak = _statTrak ? 1 : 0;

            var minimums = GetDataByCollections(ConnectionString, rarity, collections, statTrak);
            if (minimums == null)
            {
                yield break;
            }
            foreach (DataRow row in minimums.Rows)
            {
                yield return new Skin(row[5]
                     , row[4]
                     , row[6]
                     , row[1]
                     , row[0]
                     , row[8]
                     , row[2]
                     , row[3]
                     , row[7]
                     );
            }
        }
        private static DataTable GetDataByCollections(string ConnectionString, Rarity rarity, IEnumerable<string> collections, int statTrak)
        {
            if (collections.Count() == 0) return null;
            var sql = new SqlHandler(ConnectionString);
            var input = @$"select * 
                    from tradeup
                    WHERE rarityCode = '{rarity}'
                    AND statTrak  = {statTrak}
                    AND CANBEOUTPUT = 1
                    AND COLLECTIONNAME IN ({string.Join(',', collections.Select(x => "'" + x + "'"))})
                    ";
            var data = sql.SelectRows(input);
            return data.Tables[0];
        }

        private static DataTable getMinimumsByCollection(string ConnectionString, Rarity rarity, IEnumerable<string> collections,int statTrak)
        {
            if (collections.Count() == 0) return null;

            var sql = new SqlHandler(ConnectionString);
            var input = @$"select * 
                    from tradeupMiniums
                    WHERE rarityCode = '{rarity}'
                    AND statTrak  = {statTrak}
                    AND COLLECTIONNAME IN ({string.Join(',', collections.Select(x => "'" + x + "'"))})
                    ";
            var data = sql.SelectRows(input);
            return data.Tables[0];
        }
    }
}
