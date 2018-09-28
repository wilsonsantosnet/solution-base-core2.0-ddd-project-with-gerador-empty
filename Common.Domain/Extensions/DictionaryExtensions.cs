using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;


public static class DictionaryExtensions
{

    public static IEnumerable<dynamic> DictionaryToObject(this IEnumerable<Dictionary<string, object>> dict)
    {
        var result = new List<dynamic>();
        foreach (var item in dict)
        {
            result.Add(item.DictionaryToObject());
        }
        return result;
    }
    public static dynamic DictionaryToObject(this Dictionary<string, object> dict)
    {
        IDictionary<string, object> eo = new ExpandoObject() as IDictionary<string, object>;
        foreach (KeyValuePair<string, object> kvp in dict)
        {
            eo.Add(kvp);
        }
        return eo;
    }

    public static IDictionary<string, object> ToDictionary(this object model)
    {
        var dic = new Dictionary<string, object>();
        if (model.IsNotNull())
        {
            if (model is IDictionary<string, object>)
                return model as IDictionary<string, object>;

            var propsDefault = model.GetType().GetProperties();

            foreach (var item in propsDefault)
                dic.Add(item.Name, item.GetValue(model));
        }

        return dic;

    }

    public static IDictionary<string, object> AddRange(this IDictionary<string, object> defaultData, IDictionary<string, object> additionalData)
    {

        foreach (var item in additionalData)
        {
            var exists = defaultData.Where(_ => _.Key == item.Key).Any();
            if (!exists)
                defaultData.Add(item.Key, item.Value);
        }
        return defaultData;

    }

}

