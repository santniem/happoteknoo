using System.Data;

namespace NäätäETL.Scripter
{
    public class DataMapperInvoker
    {
        public DatamapperScriptResult _methods;
        public DataSet dataSet;


        public DataMapperInvoker(DatamapperScriptResult scriptResult)
        {
            _methods = scriptResult;
            dataSet = _methods.GetDataSetMethod.Invoke(null, new object[] { }) as DataSet;
        }


        public DataSet getData(object value)
        {
            clearDataSet();
            _methods.MapObjectToDataSetMethod.Invoke(null, new[] {dataSet, value});

            //https://stackoverflow.com/questions/12741284/datatable-copy-or-deepclone-which-one-to-choose
            var clone = new DataSet(dataSet
                .DataSetName); // voi iesuksen vittu ei näitä paskoja voikkaa kopioida niinku ajatteli
            foreach (DataTable item in
                     dataSet.Tables) //tua async maailmnas nää menee iha eri järjestyksee ja tulee kryptusiä erroreita
            {
                var dtCopy = item.Copy();
                clone.Tables.Add(dtCopy);
            }

            return clone;
        }

        private void clearDataSet()
        {
            foreach (DataTable item in dataSet.Tables) item.Rows.Clear();
        }
    }
}