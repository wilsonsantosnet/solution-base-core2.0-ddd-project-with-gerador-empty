using System.Collections.Generic;

namespace Common.Domain
{
    public interface IWebJobRequest
    {
        void Config(string authorityEndPoint, string apiEndPoint, string clientId, string secret, string scope = null);
        bool Enqueue(string resource, dynamic parameters);
    }
}
