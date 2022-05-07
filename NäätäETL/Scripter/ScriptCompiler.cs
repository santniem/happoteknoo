using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace NäätäETL.Scripter
{
    public static class ScriptCompiler
    {
        public static DatamapperScriptResult CompileScriptToMapper(string script)
        {
            File.WriteAllText("C:/temp/paska.cs",script);
            var resultingType = RoslynCreateType(script);
            var magicConstructor = resultingType.GetConstructor(Type.EmptyTypes);
            var result = new DatamapperScriptResult
            {
                magicClassObject = magicConstructor.Invoke(new object[] { }),
                MapObjectToDataSetMethod = resultingType.GetMethod("mapValue"),
                GetDataSetMethod = resultingType.GetMethod("makeDataset")
            };
            return result;
        }

        private static Type RoslynCreateType(string classDefinitions)
        {
            var opt = ScriptOptions.Default;
            opt.AddImports("System");
            opt.AddImports("System.Data");

            var script = CSharpScript.Create(classDefinitions,
                ScriptOptions.Default.WithReferences(Assembly.GetExecutingAssembly()));

            script.Compile();


            using var assemblyLoadContext = new CollectibleAssemblyLoadContext();
            using var stream = new MemoryStream();

            var emitResult = script.GetCompilation().Emit(stream);

            //Tässä oli semmonen homma että classDefinitions saattoi sisältää virheitä ja tällä sai katottua rivit mistä virheet löyty
            if (!emitResult.Success)
            {
                var err = "Virhe tyyppejä luotaessa \n";
                foreach (var item in emitResult.Diagnostics) err += item + "\n";
                throw new Exception(err);
            }

            stream.Seek(0, SeekOrigin.Begin);

            var asm = assemblyLoadContext.LoadFromStream(stream);

            var types = asm.GetTypes();

            //Filuja varten jotka on luotu fromSamplella.
            var type = types[1];
            return type;
        }

        private class CollectibleAssemblyLoadContext : AssemblyLoadContext, IDisposable
        {
            public CollectibleAssemblyLoadContext() : base(true)
            {
            }

            public void Dispose()
            {
                Unload();
            }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                return null;
            }
        }
    }
}