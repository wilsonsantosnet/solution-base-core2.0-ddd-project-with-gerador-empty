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
        public HttpStatusCode StatusCode { get; set; }
        public Summary Summary { get; set; }
        public ValidationSpecificationResult Result { get; set; }
        public WarningSpecificationResult Warning { get; set; }
        public ConfirmEspecificationResult Confirm { get; set; }

    }
    public class HttpResult<T> : HttpResult
    {
        private ILogger _logger;
        public HttpResult(ILogger logger)
        {
            base.Summary = new Summary();
            this._logger = logger;
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

        public HttpResult<T> Error(IList<string> erros)
        {
            this.StatusCode = HttpStatusCode.InternalServerError;
            this.Result = new ValidationSpecificationResult
            {
                Errors = erros,
                IsValid = false
            };

            return this;
        }

        public HttpResult<T> Error(string erro, ErrorMap errorMap)
        {
            var _errorMap = errorMap;
            var erroTraduction = _errorMap.GetTraduction(erro);

            var errorFinal = erroTraduction ?? erro;

            this.StatusCode = HttpStatusCode.InternalServerError;
            this.Result = new ValidationSpecificationResult
            {
                Errors = new List<string> { errorFinal },
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
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }
        public ObjectResult ReturnCustomResponse(T OneResult, FilterBase filter = null)
        {

            this.Success(OneResult);
            return new ObjectResult(this)
            {
                StatusCode = (int)this.StatusCode
            };

        }
        public ObjectResult ReturnCustomResponse(IApplicationServiceBase _app, SearchResult<T> searchResult, FilterBase filter)
        {
            this.Summary = searchResult.Summary;
            this.Warning = _app.GetDomainWarning(filter);
            this.Confirm = _app.GetDomainConfirm(filter);
            this.Result = _app.GetDomainValidation(filter);

            this.Success(searchResult.DataList);
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

        public ObjectResult ReturnCustomException(Exception ex, string appName, object model = null, ErrorMap errorMap = null)
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
            result = ExceptionWithInner(ex, appName, errorMap);


            this._logger.LogCritical("{0} - [1]", erroMessage, ex);
            return new ObjectResult(result) { StatusCode = (int)result.StatusCode };

        }

        private HttpResult<T> ExceptionWithInner(Exception ex, string appName, ErrorMap errorMap = null)
        {
            if (ex.InnerException.IsNotNull())
            {
                if (ex.InnerException.InnerException.IsNotNull())
                    return this.Error(string.Format("[{0}] - InnerException: {1}", appName, ex.InnerException.InnerException.Message), errorMap);
                else
                    return this.Error(string.Format("[{0}] - InnerException: {1}", appName, ex.InnerException.Message), errorMap);
            }
            else
            {
                return this.Error(string.Format("[{0}] - Exception: {1}", appName, ex.Message), errorMap);
            }
        }

    }
}
