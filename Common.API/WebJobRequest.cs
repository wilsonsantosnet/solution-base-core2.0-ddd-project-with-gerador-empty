using Common.Domain;
using Common.Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.API
{
    public class WebJobRequest : IWebJobRequest
    {
        private readonly IRequest _request;
        private readonly CurrentUser _user;
        private string _authorityEndPoint;
        private string _apiEndPoint;
        private string _clientId;
        private string _secret;
        private string _scope;
        private readonly ILogger _logger;
        public WebJobRequest(IRequest request, CurrentUser user, ILoggerFactory logger)
        {
            this._request = request;
            this._user = user;
            this._logger = logger.CreateLogger<WebJobRequest>();
        }

        public void Config(string authorityEndPoint, string apiEndPoint, string clientId, string secret, string scope = null)
        {
            this._authorityEndPoint = authorityEndPoint;
            this._apiEndPoint = apiEndPoint;
            this._clientId = clientId;
            this._secret = secret;
            this._scope = scope;
        }

        public bool Enqueue(string resource, dynamic parameters)
        {
            if (this._authorityEndPoint.IsNullOrEmpty() || this._apiEndPoint.IsNullOrEmpty())
                return false;

            try
            {
                this._request.ClearHeaders();
                this.DefineRequest();
                this._request.Post<dynamic, dynamic>(resource, parameters);
                return true;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return false;
            }
        }

        private void DefineRequest()
        {
            this._request.SetAddress(this._apiEndPoint);
            var accessToken = this._user.GetToken();
            this._request.SetBearerToken(accessToken);
        }

    }
}
