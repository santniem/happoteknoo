using System;
using System.Collections.Generic;
using System.Data;
using NäätäETL.Scripter;
using NäätäETL.TypeShenanigans;

namespace NäätäETL.ETLPipeLine
{
    public class DataSource
    {
        private readonly DataMapperInvoker _mapper;

        private DataSource(DatamapperScriptResult mapper)
        {
            _mapper = new DataMapperInvoker(mapper);
        }


        public DataSet getData(object rawdata)
        {
            return _mapper.getData(rawdata);
        }

        public DataSet getSchema()
        {
            return _mapper.dataSet;
        }

        public static DataSource InitializeSourceFromType(Type t)
        {
            var trees = FlattenTreeToCollections(t);
            var scripter = new DataTableMapperScriptModel(trees);
            var script = scripter.doShit();
            var mapper = ScriptCompiler.CompileScriptToMapper(script);
            return new DataSource(mapper);
        }

        public static DataMapperInvoker purkkaa(Type t)
        {
            var trees = FlattenTreeToCollections(t);
            var scripter = new DataTableMapperScriptModel(trees);
            var script = scripter.doShit();
            var mapper = ScriptCompiler.CompileScriptToMapper(script);
            return new DataMapperInvoker(mapper);
        }

        private static IEnumerable<TypeWrapper> FlattenTreeToCollections(Type t)
        {
            var flattener = new TypeFlattener(t);
            var tree = flattener.GetThings();
            var treesByCollection = flattener.RemoveUselessChildren(tree);
            foreach (var item in treesByCollection) flattener.MergeLeafToParent(item);
            return treesByCollection;
        }
    }
}