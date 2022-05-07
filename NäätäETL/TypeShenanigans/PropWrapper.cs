using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NäätäETL.TypeShenanigans
{
    public class PropWrapper
    {
        private readonly List<string> ancestorPath = new();

        private readonly List<string> propertyPath = new();
        public PropertyInfo propertyInfo;

        private bool reversed;

        public PropWrapper(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
            addPropertyPathPiece(propertyInfo.Name);
        }

        public string propType()
        {
            if (propertyInfo.PropertyType.Name != "Nullable`1")
                return propertyInfo.PropertyType.Name;
            return Nullable.GetUnderlyingType(propertyInfo.PropertyType).Name;
        }

        public void addPropertyPathPiece(string propertyPathPiece)
        {
            propertyPath.Add(propertyPathPiece);
        }

        public void AddPropertyAncestorPaths(string[] propertyPathPieces)
        {
            ancestorPath.AddRange(propertyPathPieces);
        }

        public string GetNullablePropertyPath()
        {
            if (!reversed)
            {
                propertyPath.Reverse();
                reversed = true;
            }

            return string.Join("?.", propertyPath);
        }

        public string GetPropertyPath()
        {
            if (!reversed)
            {
                propertyPath.Reverse();
                reversed = true;
            }

            return string.Join(".", propertyPath);
        }

        public string GetFieldName()
        {
            if (!reversed)
            {
                propertyPath.Reverse();
                reversed = true;
            }

            return string.Join("_", propertyPath);
        }

        public string GetAncestorPath()
        {
            return string.Join(".", ancestorPath);
        }

        public string GetAncestorPathNoRoot()
        {
            return string.Join(".", ancestorPath.Where(x => x != "root"));
        }
    }
}