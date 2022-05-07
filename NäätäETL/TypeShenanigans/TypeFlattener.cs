using System;
using System.Collections.Generic;
using System.Linq;

namespace NäätäETL.TypeShenanigans
{
    public class TypeFlattener
    {
        public TypeFlattener(Type type)
        {
            _type = type;
        }

        public Type _type { get; set; }

        public IEnumerable<TypeWrapper> SeparateByCollections()
        {
            var thing = GetThings();
            var thing2 = thing.getCollectionNodes();
            return thing2;
        }

        public TypeWrapper GetThings()
        {
            var tw = new TypeWrapper(_type, "root");
            var asd = handleComplexType(_type, "root", tw);
            asd.Parent = null;
            return asd;
        }

        public List<TypeWrapper> RemoveUselessChildren(TypeWrapper typeWrapper)
        {
            var splits = typeWrapper.getCollectionNodes().ToList();
            var ids = splits.Select(x => x.Id);
            foreach (var item in splits)
            {
                var inSomewhere = item.Children.Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToArray();
                foreach (var idToRemove in inSomewhere) item.RemovedChildByID(idToRemove);
            }

            return splits.ToList();
        }


        public void MergeLeafToParent(TypeWrapper typeWrapper)
        {
            while (typeWrapper.Children.Count > 0)
            {
                var leafs = typeWrapper.getLeafNodes().ToList();
                foreach (var leafNode in leafs) leafNode.Parent.MergeProperties(leafNode);
            }

            typeWrapper.ParentToPropsFullpath();
        }


        private TypeWrapper handleComplexType(Type type, string propertyName, TypeWrapper parent)
        {
            var tw = new TypeWrapper(type, propertyName);
            if (tw.TypePropertyName != "root") tw.Parent = parent;


            tw.Parent = parent;

            foreach (var p in tw.complexTypes)
            {
                var a = handleComplexType(p.PropertyType, p.Name, tw);
                tw.Children.Add(a);
            }

            foreach (var p in tw.complexCollections)
            {
                var listType = p.PropertyType.GetGenericArguments()[0];
                var a = handleComplexType(listType, p.Name, tw);
                //tw.isCollection = true;
                a.isCollection = true;
                tw.Children.Add(a);
            }

            return tw;
        }
    }
}