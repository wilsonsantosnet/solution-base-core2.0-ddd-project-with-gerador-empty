using Common.Domain.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Common.Domain.Model;
using System.Linq;
using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Common.Domain.Base;
using Microsoft.Extensions.Logging;

namespace Common.API
{
    public abstract class HttpResult
    {
        public HttpResult()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
        public HttpStatusCode StatusCode { get; set; }
        public Summary Summary { get; set; }
        public ValidationSpecificationResult Result { get; set; }
        public WarningSpecificationResult Warning { get; set; }
        public ConfirmEspecificationResult Confirm { get; set; }
        public string Cachekey { get; set; }
        public double CacheExpirationMinutes { get; set; }

    }
    public class HttpResult<T> : HttpResult
    {
        private readonly ILogger _logger;
        private readonly ErrorMap _errorMap;

        public HttpResult(ILogger logger, ErrorMap errorMap = null)
        {
            base.Summary = new Summary();
            this._logger = logger;
            this._errorMap = errorMap;
        }

        public IEnumerable<T> DataList { get; set; }
        public T Data { get; set; }


        public HttpResult<T> Success(T data)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Data = data;
            return this;

        }

        public HttpResult<T> Success(IEnumerable<T> dataList)
        {
            this.StatusCode = HttpStatusCode.OK;
            this.DataList = dataList;
            return this;

        }

        public HttpResult<T> Success()
        {
            this.StatusCode = HttpStatusCode.OK;
            return this;
        }

        public HttpResult<T> Error(string error)
        {
            return this.Error(new List<string> { error });
        }

        public HttpResult<T> Error(IList<string> erros)
        {
            var errosFinais = erros;
            if (this._errorMap.IsNotNull())
            {
                var erroTraduction = erros.Select(erro => this._errorMap.GetTraduction(erro)).ToList();
                if (erroTraduction.IsAny())
                    errosFinais = erroTraduction;
            }

            this.StatusCode = HttpStatusCode.InternalServerError;
            this.Result = new ValidationSpecificationResult
            {
                Errors = errosFinais,
                IsValid = false
            };

            return this;
        }

        private HttpResult<T> BadRequest(string erro)
        {
            this.StatusCode = HttpStatusCode.BadRequest;
            this.Result = new ValidationSpecificationResult
            {
                Errors = new List<string> { erro },
                IsValid = false
            };
            return this;
        }

        private HttpResult<T> NotFound(string erro)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            this.Result = new ValidationSpecificationResult
            {
                Errors = new List<string> { erro },
                IsValid = false
            };
            return this;
        }

        private HttpResult<T> AlreadyExists(string erro)
        {
            this.StatusCode = HttpStatusCode.Conflict;
            this.Result = new ValidationSpecificationResult
            {
                Errors = new List<string> { erro },
                IsValid = false
            };
            return this;
        }

        private HttpResult<T> NotAuthorized(string erro)
        {
            this.StatusCode = HttpStatusCode.Unauthorized;
            this.Result = new ValidationSpecificationResult
            {
                Errors = new List<string> { erro },
                IsValid = false
            };
            return this;
        }


        public ObjectResult ReturnCustomResponse()
        {
            this.Success();
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }

        public ObjectResult ReturnCustomResponse(IEnumerable<T> searchResult, FilterBase filter = null)
        {
            this.Success(searchResult);
            this.AddCacheInfo(filter);

            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }

      

        public ObjectResult ReturnCustomResponse(dynamic OneResult, FilterBase filter = null)
        {

            this.Success(OneResult);
            this.AddCacheInfo(filter);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }
        public ObjectResult ReturnCustomResponse(SearchResult<T> searchResult, FilterBase filter = null)
        {

            this.Summary = searchResult.Summary;
            this.Success(searchResult.DataList);
            this.AddCacheInfo(filter);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }


        public ObjectResult ReturnCustomResponse(IApplicationServiceBase _app, SearchResult<T> searchResult, FilterBase filter)
        {

            this.Warning = _app.GetDomainWarning(filter);
            this.Confirm = _app.GetDomainConfirm(filter);
            this.Result = _app.GetDomainValidation(filter);

            this.ErrorMap();

            this.Summary = searchResult.Summary;
            this.Cachekey = searchResult.Cachekey;
            this.CacheExpirationMinutes = searchResult.CacheExpirationMinutes;
            this.Success(searchResult.DataList);
            this.AddCacheInfo(filter);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };


        }

        public ObjectResult ReturnCustomResponse(IApplicationServiceBase _app, IEnumerable<T> returnModel)
        {
            foreach (var item in returnModel)
            {
                var response = ReturnCustomResponse(_app, item);
                if (response.StatusCode != (int)HttpStatusCode.OK)
                    return response;
            }

            this.Success(returnModel);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }
        public ObjectResult ReturnCustomResponse(IApplicationServiceBase _app)
        {
            return this.ReturnCustomResponse(_app, null);
        }
        public ObjectResult ReturnCustomResponse(IApplicationServiceBase _app, T returnModel)
        {
            this.Warning = _app.GetDomainWarning();
            this.Confirm = _app.GetDomainConfirm();
            this.Result = _app.GetDomainValidation();

            this.ErrorMap();

            if (this.Result.IsNotNull())
            {
                if (!this.Result.IsValid)
                {
                    return new ObjectResult(this)
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }
            }

            this.Success(returnModel);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }

        public ObjectResult ReturnCustomException(Exception ex, string appName, object model = null)
        {

            var result = default(HttpResult<T>);

            if ((ex as CustomNotFoundException).IsNotNull())
            {
                result = this.NotFound(ex.Message);
            }

            if ((ex as CustomBadRequestException).IsNotNull())
            {
                result = this.BadRequest(ex.Message);
            }

            if ((ex as CustomNotAutorizedException).IsNotNull())
            {
                result = this.NotAuthorized(ex.Message);
            }

            if ((ex as CustomAlreadyExistsException).IsNotNull())
            {
                result = this.AlreadyExists(ex.Message);
            }

            if ((ex as CustomValidationException).IsNotNull())
            {
                var customEx = ex as CustomValidationException;
                result = this.Error(customEx.Errors);
            }

            var erroMessage = ex.Message;
            if (model.IsNotNull())
            {
                var modelSerialization = JsonConvert.SerializeObject(model);
                erroMessage = string.Format("[{0}] - {1} - [{2}]", appName, ex.Message, modelSerialization);
            }
            result = ExceptionWithInner(ex, appName);


            this._logger.LogCritical("{0} - [1]", erroMessage, ex);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };

        }

        private void AddCacheInfo(FilterBase filter)
        {
            if (filter.IsNotNull())
            {
                this.Cachekey = filter.FilterKey;
                this.CacheExpirationMinutes = filter.CacheExpiresTime.TotalMinutes;
            }
        }

        private HttpResult<T> ExceptionWithInner(Exception ex, string appName)
        {
            if (ex.InnerException.IsNotNull())
            {
                if (ex.InnerException.InnerException.IsNotNull())
                    return this.Error(string.Format("[{0}] - InnerException: {1}", appName, ex.InnerException.InnerException.Message));
                else
                    return this.Error(string.Format("[{0}] - InnerException: {1}", appName, ex.InnerException.Message));
            }
            else
            {
                return this.Error(string.Format("[{0}] - Exception: {1}", appName, ex.Message));
            }
        }

        private void ErrorMap()
        {
            if (this._errorMap.IsNotNull())
            {
                if (this.Warning.IsNotNull() && this.Warning.Warnings.IsAny()) this.Warning.Warnings = this.Warning.Warnings.Select(_ => this._errorMap.GetTraduction(_));
                if (this.Confirm.IsNotNull() && this.Confirm.Confirms.IsAny()) this.Confirm.Confirms = this.Confirm.Confirms.Select(vc => new ValidationConfirm(vc.VerifyBehavior, this._errorMap.GetTraduction(vc.Message)));
                if (this.Result.IsNotNull() && this.Result.Errors.IsAny()) this.Result.Errors = this.Result.Errors.Select(_ => this._errorMap.GetTraduction(_));
            }
        }

    }
}