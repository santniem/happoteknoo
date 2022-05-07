using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NäätäETL.TypeShenanigans
{
    public static class TypeHelpers
    {
        public static object GetPropValue(this object src, string propName)
        {
            try
            {
                return src.GetType().GetProperty(propName)?.GetValue(src, null);
            }
            catch (Exception e)
            {
                return typeof(string);
            }
        }

        public static bool IsSimple(this Type type)
        {
            if (type == null) return false;
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>))
                // nullable type, check if the nested type is simple.
                return typeInfo.GetGenericArguments()[0].IsSimple();

            return typeInfo.IsPrimitive
                   || typeInfo.IsEnum
                   || type == typeof(string)
                   || type == typeof(DateTimeOffset)
                   || type == typeof(DateTime)
                   || type == typeof(decimal);
        }

        //Miksen mä oo tätät tajunnu käyttää
        public static bool TypeContainsCollection(this Type type)
        {
            var wasd = type.GetProperties();
            foreach (var p in type.GetProperties().Where(x => !x.PropertyType.IsSimple()))
            {
                // Is collection?
                var value = p.PropertyType;
                var isCollection = typeof(IEnumerable).IsAssignableFrom(value);
                if (isCollection) return true;
            }

            return false;
        }

        public static bool TypeIsCollection(this Type type)
        {
            var isCollection = typeof(IEnumerable).IsAssignableFrom(type);
            return isCollection;
        }

        public static Type GetAnyElementType(Type type)
        {
            // Type is Array
            // short-circuit if you expect lots of arrays 
            if (type.IsArray)
                return type.GetElementType();

            // type is IEnumerable<T>;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            // type implements/extends IEnumerable<T>;
            var enumType = type.GetInterfaces()
                .Where(t => t.IsGenericType &&
                            t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(t => t.GenericTypeArguments[0]).FirstOrDefault();
            return enumType ?? type;
        }
    }
}