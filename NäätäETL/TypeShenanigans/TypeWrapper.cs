using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NäätäETL.TypeShenanigans
{
    public class TypeWrapper
    {
        private string _typePropertyName;

        public TypeWrapper(Type type, string typePropertyName)
        {
            this.type = type;
            TypePropertyName = typePropertyName;
            properties = type.GetProperties().Where(x => !x.PropertyType.IsEnum).ToList();
            propWrappers = simpleProperties.Select(x => new PropWrapper(x)).ToList();
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public Type type { get; set; }

        public string TypePropertyName
        {
            get
            {
                if (isRoot) return type.Name;
                return _typePropertyName;
            }
            set => _typePropertyName = value;
        }

        private List<PropertyInfo> properties { get; }
        public IEnumerable<PropertyInfo> simpleProperties => properties.Where(x => x.PropertyType.IsSimple());

        public IEnumerable<PropertyInfo> complexTypes =>
            properties.Where(x => !x.PropertyType.IsSimple() && !x.PropertyType.TypeIsCollection());

        public IEnumerable<PropertyInfo> simpleCollections => properties
            .Where(x => !x.PropertyType.IsSimple() && x.PropertyType.TypeIsCollection())
            .Where(x => x.PropertyType.GetElementType().IsSimple());

        public IEnumerable<PropertyInfo> complexCollections => properties
            .Where(x => !x.PropertyType.IsSimple() && x.PropertyType.TypeIsCollection())
            .Where(x => x.PropertyType.GetGenericArguments().Count() > 0) //nullcheck
            .Where(x => !x.PropertyType.GetGenericArguments()[0] //otetaan listan tyyppi
                .IsSimple());


        public bool isCollection { get; set; } = false;
        public bool ParentIsCollection { get; set; } = false;
        public bool isLeaf => Children.Count == 0;
        public bool isRoot => Parent == null;


        public TypeWrapper Parent { get; set; }
        public List<TypeWrapper> Children { get; set; } = new();

        public List<PropWrapper> propWrappers { get; set; } = new();


        public string[] GetAncestorPropertyNames()
        {
            if (isRoot) return new[] {"root"};

            var parent = Parent;
            var parentPropertyNames = parent.GetAncestorPropertyNames();
            var thisPropertyName = TypePropertyName;
            var thisPropertyNames = new[] {thisPropertyName};
            return parentPropertyNames.Concat(thisPropertyNames).ToArray();
        }

        public void ParentToPropsFullpath()
        {
            foreach (var prop in propWrappers) prop.AddPropertyAncestorPaths(GetAncestorPropertyNames());
        }

        public void MergeProperties(TypeWrapper wrapper)
        {
            if (wrapper.isCollection)
            {
                RemovedChildByID(wrapper.Id);
                return;
            }

            foreach (var item in wrapper.propWrappers) item.addPropertyPathPiece(wrapper.TypePropertyName);


            properties.AddRange(wrapper.properties); //Add Childs simple properties to current item props
            propWrappers.AddRange(wrapper.propWrappers); //Add Childs simple properties to current item props
            RemovedChildByID(wrapper.Id); //Remove child from parent
        }


        public void RemovedChildByID(Guid idToRemove)
        {
            Children.RemoveAll(x => x.Id == idToRemove);
        }

        public IEnumerable<TypeWrapper> getCollectionNodes()
        {
            if (isRoot) yield return this;
            foreach (var item in Children)
            {
                if (item.isCollection) yield return item;


                foreach (var leaf in item.getCollectionNodes()) yield return leaf;
            }
        }


        public IEnumerable<TypeWrapper> getLeafNodes()
        {
            foreach (var item in Children)
                if (item.isLeaf)
                    yield return item;
                else
                    foreach (var leaf in item.getLeafNodes())
                        yield return leaf;
        }
    }
}