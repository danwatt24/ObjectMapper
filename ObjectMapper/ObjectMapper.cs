using System.Reflection;

namespace ObjectMapper
{
    public class Mapper
    {
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> PROPS;
        static Mapper()
        {
            PROPS = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
        }

        public static To Map<From, To>(From a)
        {
            var fromProps = getProps<From>();
            var toProps = getProps<To>();
            var result = Activator.CreateInstance<To>();

            foreach (var toProp in toProps)
            {
                if (!fromProps.TryGetValue(toProp.Key, out var from)) continue;
                toProp.Value.SetValue(result, from.GetValue(a));
            }

            return result;
        }

        public static void Register<T>() => Register(typeof(T));
        public static void Register(Type t) => saveProps(t);

        private static Dictionary<string, PropertyInfo> getProps<T>() => getProps(typeof(T));
        private static Dictionary<string, PropertyInfo> getProps(Type t)
        {
            saveProps(t);
            return PROPS[t];
        }

        private static void saveProps(Type t)
        {
            if (!PROPS.ContainsKey(t))
            {
                var props = t.GetProperties().ToDictionary(p => p.Name, p => p);
                PROPS.Add(t, props);
            }
        }
    }
}
