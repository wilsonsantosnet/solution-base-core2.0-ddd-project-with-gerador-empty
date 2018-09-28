using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.API
{
    public class DateTimePtBrConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
			if (reader.Value.IsNull())
	    		return null;

	   		if (DateTime.TryParse(reader.Value.ToString(), out DateTime data))
	    		return data;

			return null;
		
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("dd/MM/yyyy HH:mm"));
        }
    }
}
