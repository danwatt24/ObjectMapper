using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

/*
 * TODO LIST:
 *      provide methods to "pre-register" mapping types
 *      create memoized version of property getters and setters
 *          to avoid having to use reflection every time. (this dependent on the previous item)
 *      figure out how to return a builder object to allow for custom mappings (CustomMappingTests)
 *      create benchmark "tests" to compare to automapper
*/
namespace ObjectMapper
{
    public class ObjectMapper : IObjectMapper
    {
        private static readonly Dictionary<Type, Func<object>> Creators;
        static ObjectMapper()
        {
            Creators = new Dictionary<Type, Func<object>>();
        }

        public Dest Map<Dest>(object source) => (Dest)Map(source, typeof(Dest));

        public object Map(object source, Type mapTo)
        {
            if (source == null) return null;

            var dest = GetCreator(mapTo)();
            var sourceProps = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProps = dest.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var sourceProp in sourceProps)
            {
                var value = sourceProp.GetValue(source);
                var destProp = destProps.FirstOrDefault(p => p.Name == sourceProp.Name);
                if (destProp != null)
                {
                    var sourceType = sourceProp.PropertyType;
                    var destType = destProp.PropertyType;
                    if (destType.IsAssignableFrom(sourceType))
                    {
                        if (sourceType.IsArray)
                            value = ArrayCopy(value);

                        destProp.SetValue(dest, value);
                    }
                }
            }

            return dest;
        }

        private object ArrayCopy(object arr) => ArrayCopy((Array)arr);
        private Array ArrayCopy(Array arr)
        {
            var type = arr.GetType();
            var elementType = type.GetElementType();
            var nArr = Array.CreateInstance(type.GetElementType(), arr.Length);
            if (elementType.IsPrimitive)
                Array.Copy(arr, nArr, arr.Length);
            else
            {
                for (var i = 0; i < nArr.Length; i++)
                {
                    var value = arr.GetValue(i);
                    var nValue = Map(value, elementType);
                    nArr.SetValue(nValue, i);
                }
            }
            return nArr;
        }

        private Func<object> GetCreator(Type t)
        {
            if (!Creators.ContainsKey(t))
                // https://stackoverflow.com/a/29972767/6038906
                Creators.Add(t, Expression.Lambda<Func<object>>(
                    Expression.New(t.GetConstructor(Type.EmptyTypes))
                ).Compile());

            return Creators[t];
        }
    }
}
