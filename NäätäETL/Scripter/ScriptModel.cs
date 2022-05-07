using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NäätäETL.TypeShenanigans;

namespace NäätäETL.Scripter
{
    public class DataTableMapperScriptModel
    {
        private const string myFunctionName = "MapAtoB";
        private readonly IEnumerable<TypeWrapper> typeWrappers;

        public DataTableMapperScriptModel(IEnumerable<TypeWrapper> typeWrappers)
        {
            this.typeWrappers = typeWrappers;
        }

        public string doShit()
        {
            var asd = ScriptMaster(typeWrappers);

            return asd;
        }

        public string ScriptMaster(IEnumerable<TypeWrapper> typeWrappers)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Data;");

            sb.AppendLine($"public class {typeWrappers.First().type.Name}Mapper {{");

            sb.AppendLine(@"    public static DataSet mapValue(DataSet ds,object juttu)
                                {
                                    " + typeWrappers.First().type.Name + @".ValueMapper(ds, juttu);
                                    return ds;
                                }");

            sb.AppendLine(scriptDataSet(typeWrappers));

            foreach (var tw in typeWrappers)
            {
                sb.AppendLine(scriptTargetType(tw));
            }
            
            sb.AppendLine("}");

            return sb.ToString();
        }


        public string scriptDataSet(IEnumerable<TypeWrapper> typeWrappers)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t public static DataSet makeDataset(){");
            sb.AppendLine($"var ds = new DataSet(\"{typeWrappers.First().TypePropertyName}\");");
            foreach (var tw in typeWrappers)
            {
                sb.AppendLine($"\t\t // {tw.TypePropertyName} TABLE DEFINITION");
                sb.AppendLine($"\t\t DataTable dt_{tw.TypePropertyName} = new DataTable(\"{tw.TypePropertyName}\");");

                foreach (var prop in tw.propWrappers)
                    sb.AppendLine(
                        $"\t\t dt_{tw.TypePropertyName}.Columns.Add(\"{prop.GetFieldName()}\",typeof({prop.propType()}));"); //dt_nimi.Columns.Add(prop,typeof(Type));

                //RELAATIOKSI GUIDEJA
                if (tw == typeWrappers.First())
                {
                    sb.AppendLine(
                        $"\t\t dt_{tw.TypePropertyName}.Columns.Add(\"{tw.TypePropertyName}GUID\",typeof({typeof(Guid)}));"); //dt_nimi.Columns.Add(thisGUID,typeof(GUID));
                }
                else
                {
                    sb.AppendLine(
                        $"\t\t dt_{tw.TypePropertyName}.Columns.Add(\"{tw.Parent.TypePropertyName}GUID\",typeof({typeof(Guid)}));"); //dt_nimi.Columns.Add(parentGUID,typeof(Type));
                    sb.AppendLine(
                        $"\t\t dt_{tw.TypePropertyName}.Columns.Add(\"{tw.TypePropertyName}GUID\",typeof({typeof(Guid)}));"); //dt_nimi.Columns.Add(thisGUID,typeof(GUID));
                }

                sb.AppendLine();
                sb.AppendLine($"\t\t ds.Tables.Add(dt_{tw.TypePropertyName});");
            }

            sb.AppendLine("\t return ds;");
            sb.AppendLine("\t}");
            return sb.ToString();
        }

        public string scriptBasicValueMapper(TypeWrapper tw)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\tpublic static void ValueMapper(DataSet ds,dynamic a) {");
            sb.AppendLine($"\t\t\tvar row = ds.Tables[\"{tw.type.Name}\"].NewRow();");
            sb.AppendLine("\t\t\t var guid = Guid.NewGuid();"); // var guid = Guid.NewGuid();
            
            //NULL CHECK
            sb.AppendLine("\t\t\t\t if(a == null){return;}");
            
            //Fill datatable values
            foreach (var prop in tw.propWrappers)
                sb.AppendLine(
                    $"\t\t\t row[\"{prop.GetFieldName()}\"] = a.{prop.GetNullablePropertyPath()} ?? DBNull.Value;"); // row[fieldName] = a.NullablePropertyPath ?? DBNULL.Value
            sb.AppendLine($"\t\t\t row[\"{tw.TypePropertyName}GUID\"] = guid;"); // row[thisGuid] = guid;

            sb.AppendLine($"\t\t\tds.Tables[\"{tw.type.Name}\"].Rows.Add(row);"); // Add row to datatable

            foreach (var item in tw.complexCollections)
            {
                var propTypes = item.PropertyType.GetGenericArguments();
                var aaaa = typeWrappers.First(x => x.type.Name == propTypes[0].Name);
                var bbb = aaaa.propWrappers.First();
                sb.AppendLine($"\t\t\t {propTypes[0].Name}.ValueMapper(ds, a.{bbb.GetAncestorPathNoRoot()},guid );");
            }


            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        public string scriptIteratingValueMapper(TypeWrapper tw)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\tpublic static void ValueMapper(DataSet ds, dynamic iterable, Guid parentGuid) {");
            
            //NULL CHECK
            sb.AppendLine("\t\t\t\t if(iterable == null){return;}");
            //Loopataan iteroitavat
            sb.AppendLine("\t\t\t foreach (var item in iterable){"); //FOREACH
            sb.AppendLine($"\t\t\t\t var row = ds.Tables[\"{tw.TypePropertyName}\"].NewRow();"); // NEW ROW
            sb.AppendLine("\t\t\t\t var guid = Guid.NewGuid();"); // var guid = Guid.NewGuid();

            sb.AppendLine($"\t\t\t\t\t row[\"{tw.TypePropertyName}GUID\"] = guid;"); // row[thisGuid] = guid;
            sb.AppendLine(
                $"\t\t\t\t\t row[\"{tw.Parent.TypePropertyName}GUID\"] = parentGuid;"); // row[parentGuid] = parentGuid;

            foreach (var prop in tw.propWrappers)
                sb.AppendLine(
                    $"\t\t\t\t\t row[\"{prop.GetFieldName()}\"] = item.{prop.GetNullablePropertyPath()} ?? DBNull.Value;");

            sb.AppendLine($"\t\t\t\t\t ds.Tables[\"{tw.TypePropertyName}\"].Rows.Add(row);");


            foreach (var item in tw.complexCollections)
            {
                var asd = item.PropertyType.GetGenericArguments();
                sb.AppendLine($"\t\t\t {asd[0].Name}.ValueMapper(ds, item.{item.Name}, guid );");
            }


            sb.AppendLine("\t\t\t}");

            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        private Dictionary<string, string> typeNameCache = new Dictionary<string, string>();
        public string scriptTargetType(TypeWrapper tw)
        {
            if (typeNameCache.ContainsKey(tw.type.Name))
            {
                return "";
            }
            var sb = new StringBuilder();
            sb.AppendLine($"\tpublic static class {tw.type.Name} {{");

            if (tw.isCollection)
                sb.AppendLine(scriptIteratingValueMapper(tw));
            else
                sb.AppendLine(scriptBasicValueMapper(tw));

            typeNameCache.Add(tw.type.Name,"jaska");
            sb.AppendLine("\t}");
            return sb.ToString();
        }

        public string scriptDataMapperFunc(TypeWrapper tw)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\tpublic static object[] MapAtoB (dynamic a) {");

            foreach (var property in tw.propWrappers)
                sb.AppendLine($"\t\t\t\t a.{property.GetNullablePropertyPath()},");
            sb.AppendLine("\t\t\t};");

            sb.AppendLine("\t\t return ret;");
            sb.AppendLine("\t\t}");
            return sb.ToString();
        }
    }
}