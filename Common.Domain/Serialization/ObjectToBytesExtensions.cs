using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Common.Domain.Serialization
{
    public static class ObjectToBytesExtensions
    {

        public static byte[] ToBytes(this object value)
        {

            var resultJson = JsonConvert.SerializeObject(value);
            var resultBytes = Encoding.UTF8.GetBytes(resultJson);

            return resultBytes;

        }

        public static object ToObject(this byte[] value)
        {

            string resultJson = Encoding.UTF8.GetString(value);
            var resultObject = JsonConvert.DeserializeObject(resultJson);
            return resultObject;

        }

    }
}
