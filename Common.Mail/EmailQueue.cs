using Common.Domain;
using Common.Domain.Base;
using Common.Domain.Enums;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Common.Mail
{
    public class EmailQueue : IEmailQueue
    {
        private string _templateId;

        private readonly IWebJobRequest _webJobRequest;
        private readonly ILogger _logger;
        private readonly ICircuitBreaker _CircuitBreaker;

        private readonly ConfigSettingsExternalRequest _configSettingsExternalRequest;
        private readonly ConfigSettingsBase _configSettingsBase;
        private readonly ConfigCircuitBreaker _configCircuitBreaker;
        private readonly CurrentUser _user;

        private KeyValuePair<string, string> _addressFrom;
        private KeyValuePair<string, string> _addressReplyTo;
        private Dictionary<string, string> _addressTo;
        private Dictionary<string, string> _bcc;
        private Dictionary<string, object> _addParameters;
        private ConfigEmailBase _configEmailBase;


        public EmailQueue(
            IWebJobRequest webJobRequest,
            ICircuitBreaker CircuitBreaker,
            CurrentUser user,
            IOptions<ConfigSettingsExternalRequest> configSettingsExternalRequest,
            IOptions<ConfigSettingsBase> configSettingsBase,
            IOptions<ConfigCircuitBreaker> configCircuitBreaker,
            IOptions<ConfigEmailBase> configEmailBase,
            ILoggerFactory iLoggerFactory)
        {
            this._webJobRequest = webJobRequest;
            this._configSettingsExternalRequest = configSettingsExternalRequest.Value;
            this._configSettingsBase = configSettingsBase.Value;
            this._configCircuitBreaker = configCircuitBreaker.Value;
            this._configEmailBase = configEmailBase.Value;
            this._CircuitBreaker = CircuitBreaker;
            this._user = user;
            this._logger = iLoggerFactory.CreateLogger(this.GetType().Name);
            this.Init();
        }

        public void Config(string endpoint, string key, string templateId)
        {
            this._templateId = templateId;
            this._addressFrom = new KeyValuePair<string, string>();
            this._addressReplyTo = new KeyValuePair<string, string>();
            this._addressTo = new Dictionary<string, string>();
            this._bcc = new Dictionary<string, string>();
            this.Init();
        }

        public void Config(ConfigEmailBase config)
        {
            this._configEmailBase = config;
        }

        private void Init()
        {
            this._addressFrom = new KeyValuePair<string, string>();
            this._addressTo = new Dictionary<string, string>();
            this._bcc = new Dictionary<string, string>();
            this._addParameters = new Dictionary<string, object>();
        }

        public void AddAddressFrom(string email, string name)
        {
            if (this._addressFrom.Key.IsNullOrEmpty())
                this._addressFrom = new KeyValuePair<string, string>(email, name);
        }
        public void AddAddressReplyTo(string email, string name)
        {
            if (this._addressReplyTo.Key.IsNullOrEmpty())
                this._addressReplyTo = new KeyValuePair<string, string>(email, name);
        }

        public void AddAddressTo(string email, string name)
        {
            if (!this._addressTo.ContainsKey(email))
                this._addressTo.Add(email, name);
        }

        public void AddBcc(string email, string name)
        {
            if (!this._bcc.ContainsKey(email))
                this._bcc.Add(email, name);
        }

        public void AddParameters(string key, string value)
        {
            if (!this._addParameters.ContainsKey(key))
                this._addParameters.Add(key, value);
        }

        public InfoResult Send(string subject, string content, Func<CircuitBreakerParameters, bool> onError, Func<string, int> onCheckCountException)
        {
            try
            {
                if (SholdWait(onCheckCountException))
                {
                    return new InfoResult
                    {
                        Type = EInfoResult.Wait,
                    };
                }

                this._webJobRequest.Config(this._configSettingsBase.AuthorityEndPoint,
                                       this._configSettingsExternalRequest.EndPointHangfire,
                                       this._configSettingsExternalRequest.ClientIdHangfire,
                                       this._configSettingsExternalRequest.SecretHangfire,
                                       this._configSettingsExternalRequest.ScopeHangfire);

                var result = this._webJobRequest.Enqueue("EnqueueMail", new
                {
                    TemplateId = this._templateId,
                    AddressFrom = this._addressFrom,
                    AddressReplyTo = this._addressReplyTo,
                    AddressTo = this._addressTo,
                    Bcc = this._bcc,
                    Parameters = this._addParameters,
                    Subject = subject,
                    Content = content,
                    UserCreateId = this._user.GetSubjectId<int>(),
                    UserCreateDate = DateTime.Now
                });

                if (!result)
                    return new InfoResult { Type = EInfoResult.Error, GeneralInfo = "Enqueue Error" };

                return new InfoResult
                {
                    Type = EInfoResult.Success,
                    GeneralInfo = "Enqueue Success"
                };

            }
            catch (Exception ex)
            {
                onError(new CircuitBreakerParameters
                {
                    Exception = ex,
                    Process = ESecurityProcess.Email.ToString()
                });
                this._logger.LogError(ex, ex.Message);
                return new InfoResult
                {
                    Type = EInfoResult.Error,
                    GeneralInfo = ex.Message
                };
            }
        }

        private bool SholdWait(Func<string, int> onCheckCountException)
        {
            return this._configCircuitBreaker.Retrys > 0 &&
                    onCheckCountException(ESecurityProcess.Email.ToString()) >= 0 &&
                    onCheckCountException(ESecurityProcess.Email.ToString()) >= this._configCircuitBreaker.Retrys;
        }

        public InfoResult Send(string subject, string content)
        {
            return Send(subject, content, onError: parameters =>
            {
                return this._CircuitBreaker.ExecuteException(parameters);
            },
            onCheckCountException: (securityProcess) =>
            {
                return this._CircuitBreaker.CountException(securityProcess);
            });
        }
    }
}
