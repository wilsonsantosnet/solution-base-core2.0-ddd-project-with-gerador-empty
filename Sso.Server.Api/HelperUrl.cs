using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public static class HelperUrl
    {

        public static string GetRedirectDomain(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
                return null;

            if (returnUrl.IndexOf("redirect_uri") == -1)
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).IsNullOrEmpty())
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).Split("=").IsNotAny())
                return null;

            var domainCoded = returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).Split("=")[1];

            var domainDecoded = WebUtility.UrlDecode(domainCoded);

            var domain = domainDecoded.Replace("&response_type", "");

            if (domain.Replace("http://", string.Empty).Replace("https://", string.Empty).Contains(":"))
            {
                var port = domain.Replace("http://", string.Empty).Replace("https://", string.Empty).Split(":")[1];
                return domain.Replace($":{port}", string.Empty);
            }


            return domain;
        }

        public static string GetRedirectUrl(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
                return null;

            if (returnUrl.IndexOf("redirect_uri") == -1)
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).IsNullOrEmpty())
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).Split("=").IsNotAny())
                return null;

            var domainCoded = returnUrl.Substring(returnUrl.IndexOf("redirect_uri")).Split("=")[1];

            var domainDecode = WebUtility.UrlDecode(domainCoded);

            return domainDecode.Replace("&response_type", "");

        }

        public static string GetClientId(string returnUrl)
        {
            if (returnUrl.IsNullOrEmpty())
                return null;

            if (returnUrl.IndexOf("client_id") == -1)
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("client_id")).IsNullOrEmpty())
                return null;

            if (returnUrl.Substring(returnUrl.IndexOf("client_id")).Split("=").IsNotAny())
                return null;

            var clientIdCoded = returnUrl.Substring(returnUrl.IndexOf("client_id")).Split("=")[1];

            var clientId = WebUtility.UrlDecode(clientIdCoded);

            clientId = clientId.Replace("&redirect_uri", "");

            return clientId;
        }

    }
}
