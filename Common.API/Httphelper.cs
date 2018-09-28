using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Common.Api
{
    public static class HttpHelper
    {

        public static string ToQueryString(this NameValueCollection collection)
        {
            var url = String.Format("?{0}", String.Join("&", collection.AllKeys
                .Where(key => collection.GetValues(key) != null)
                        .SelectMany(key => collection.GetValues(key)
                            .Select(value => String.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value))))
                            .ToArray()));

            return url;
        }

    }
}
