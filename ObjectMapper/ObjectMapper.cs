using System;
using System.Linq;
using System.Reflection;

namespace ObjectMapper
{
    public class ObjectMapper : IObjectMapper
    {
        public T map<T>(object obj) where T : new()
        {
            if (obj == null) return default;

            var nObj = new T();
            var sourceProps = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var destProps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var sourceProp in sourceProps)
            {
                var value = sourceProp.GetValue(obj);
                var destProp = destProps.FirstOrDefault(p => p.Name == sourceProp.Name);
                if (destProp != null)
                {
                    var sourceType = sourceProp.PropertyType;
                    var destType = destProp.PropertyType;
                    if (destType.IsAssignableFrom(sourceType))
                    {
                        if (sourceType.IsArray)
                            value = ArrayCopy(value);

                        destProp.SetValue(nObj, value);
                    }
                }
            }

            return nObj;
        }

        

        protected object ArrayCopy(object obj) => ArrayCopy((Array)obj);
        protected Array ArrayCopy(Array arr)
        {
            var type = arr.GetType();
            var nArr = Array.CreateInstance(type.GetElementType(), arr.Length);
            Array.Copy(arr, nArr, arr.Length);
            return nArr;
        }
    }
}
