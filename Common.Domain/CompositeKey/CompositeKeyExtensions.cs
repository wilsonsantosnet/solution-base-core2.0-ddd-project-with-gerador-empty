using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Domain.CompositeKey
{
    public static class CompositeKeyExtensions
    {

        public static string MakeCompositeKey(this object source, string sufixKey = null, Boolean verboseKey=false)
        {

            var propertys = source.GetType().GetTypeInfo().GetProperties();
            var keys = new List<string>
            {
                $"{source.GetType().Name}:"
            };

            foreach (var item in propertys)
            {
                var propertyValue = item.GetValue(source);
                var propertyName = verboseKey ? item.Name : string.Empty;
                if (propertyValue != null)
                {
                    if (propertyName == "FilterKey")
                        continue;

                    if (item.PropertyType == typeof(int) && Convert.ToInt32(propertyValue) != default(int))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(int?) && (int?)propertyValue != default(int?))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(decimal) && Convert.ToDecimal(propertyValue) != default(decimal))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(decimal?) && (decimal?)propertyValue != default(decimal?))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(string) && propertyValue.ToString().IsNotNullOrEmpty())
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(DateTime) && Convert.ToDateTime(propertyValue) != default(DateTime))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType == typeof(DateTime?) && (DateTime?)propertyValue != default(DateTime?))
                        keys.Add($"{propertyName}:{propertyValue}");

                    if (item.PropertyType.IsEnum)
                        keys.Add($"{propertyName}:{propertyValue}");
                }

            }

            var uniqueKeys = keys.GroupBy(_ => _).Select(_ => _.Key);
            return MakeFinalKey(sufixKey, keys);


        }

        private static string MakeFinalKey(string sufixKey, IEnumerable<string> keys)
        {
            if (!string.IsNullOrEmpty(sufixKey))
                return ($"{CompositeKey(keys.ToArray())}Sufix:{sufixKey}").ToUpper();

            return ($"{CompositeKey(keys.ToArray())}").ToUpper();
        }

        private static string CompositeKey(object[] keys)
        {
            var key = string.Empty;
            keys.ToList().ForEach(_ =>
            {
                if (_ != null)
                    key += _.ToString();
            });

            return key;
        }
    }
}
