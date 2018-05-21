using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Security
{
    public static class Base64
    {

        public static string Base64Encoding(string source)
        {

            var encoding = new System.Text.ASCIIEncoding();
            var bytes = encoding.GetBytes(source);
            var s = Convert.ToBase64String(bytes);
            return s;

        }

    }
}
