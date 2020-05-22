using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.API
{
    public class DecimalPtBrConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
             return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			if (reader.Value.IsNull())
	    		return null;

	   		if (Decimal.TryParse(reader.Value.ToString(), out decimal data))
	    		return data;

			return null;
		
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString().Replace(".",","));
        }
    }
}
