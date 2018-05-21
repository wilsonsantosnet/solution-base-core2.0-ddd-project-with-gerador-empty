using System.Collections.Generic;

namespace Common.Domain
{
    public interface IRequest
    {
        void SetAddress(string address);
        void AddHeaders(string item);
        void AddHeaders(string key, string value);
        void SetBearerToken(string accessToken);
        TResult Get<TResult>(string resource, IDictionary<string, object> queryStringParameters = null);
        TResult Post<TResult, TModel>(string resource, TModel model);
        TResult Path<TResult, TModel>(string resource, TModel model);
        string GetAccessToken(string AuthorityEndPoint, string clientId, string secret, string scope = null);
    }
}
