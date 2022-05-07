using System.Reflection;

namespace NäätäETL.Scripter
{
    public class DatamapperScriptResult
    {
        public object magicClassObject;
        public MethodInfo MapObjectToDataSetMethod { get; set; }
        public MethodInfo GetDataSetMethod { get; set; }
    }
}