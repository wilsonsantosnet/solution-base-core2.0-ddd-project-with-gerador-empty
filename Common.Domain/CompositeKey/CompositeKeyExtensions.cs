using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Domain.CompositeKey
{
    public static class CompositeKeyExtensions
    {

        public static string CompositeKey(this object source)
        {

            var propertys = source.GetType().GetTypeInfo().GetProperties();
            var keys = new List<object>();
            foreach (var item in propertys)
            {
                var propertyValue = item.GetValue(source);

                if (propertyValue != null)
                {
                    if (item.PropertyType == typeof(int) && Convert.ToInt32(propertyValue) != default(int))
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(int?) && (int?)propertyValue != default(int?))
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(decimal) && Convert.ToDecimal(propertyValue) != default(decimal))
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(decimal?) && (decimal?)propertyValue != default(decimal?))
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(string) && propertyValue.ToString().IsNotNullOrEmpty())
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(DateTime) && Convert.ToDateTime(propertyValue) != default(DateTime))
                        keys.Add(propertyValue);

                    if (item.PropertyType == typeof(DateTime?) && (DateTime?)propertyValue != default(DateTime?))
                        keys.Add(propertyValue);
                }

            }
            keys.Add(source.GetType().Name);
            return CompositeKey(keys.ToArray());


        }
        private static string CompositeKey(object[] keys)
        {
            var key = string.Empty;
            keys.ToList().ForEach(_ =>
            {
                if (_ != null)
                    key += _.ToString();
            });

            if (key.Length > 250)
                return key.Substring(0, 249);

            return key;
        }
    }
}
