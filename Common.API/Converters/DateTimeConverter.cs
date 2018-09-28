using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Common.API.Converters
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;

            if (date.Hour == 0 && date.Minute == 0)
                writer.WriteValue(date.ToString("yyyy-MM-dd"));
            else
                writer.WriteValue(value);
        }
    }
}
