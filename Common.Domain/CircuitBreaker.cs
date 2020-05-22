using Common.Domain.Base;
using Common.Domain.Enums;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Common.Domain
{
    public class CircuitBreaker : ICircuitBreaker
    {
        private readonly CurrentUser _user;
        private readonly ILogger _logger;
        private readonly ConfigCircuitBreaker _configCircuitBreaker;

        public CircuitBreaker(
            ILoggerFactory logger,
            IOptions<ConfigCircuitBreaker> configCircuitBreaker,
            CurrentUser user)
        {
            this._logger = logger.CreateLogger("CircuitBreakerCommand");
            this._user = user;
            this._configCircuitBreaker = configCircuitBreaker.Value;
        }

        public int CountException(string CircuitProcess)
        {
            var sp = this._user.GetCircuitBreaker().Where(_ => _.Process == CircuitProcess).SingleOrDefault();
            if (sp.IsNotNull())
            {
                var timeElapsed = DateTime.Now.ToTimeZone().Subtract(sp.DateStop).Minutes;
                if (timeElapsed > this._configCircuitBreaker.TimeElapsed)
                    sp.ClearErrorCount();

                return sp.Exception;
            }

            return 0;
        }

        public bool ExecuteException(CircuitBreakerParameters model)
        {
            try
            {
                if (model.Exception.IsNotNull())
                {
                    var processExists = this._user.GetCircuitBreaker().Where(_ => _.Process == model.Process).SingleOrDefault();
                    if (processExists.IsNull())
                    {
                        var CircuitBreaker = new CurrentUser.CircuitBreakerMananger();
                        CircuitBreaker.SetProcess(model.Process);
                        CircuitBreaker.AddErrorCount();
                        CircuitBreaker.SetSecurityDateStop(DateTime.Now.ToTimeZone());
                        this._user.AddCircuitBreaker(CircuitBreaker);
                    }
                    else
                    {
                        processExists.AddErrorCount();
                        processExists.SetSecurityDateStop(DateTime.Now.ToTimeZone());
                    }
                }

            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());

            }
            return true;
        }

}}
