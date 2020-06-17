using Hangfire;
using Microsoft.Extensions.Logging;
using Seed.HangFire.Interfaces;
using System;

namespace Seed.HangFire.Base
{
    public abstract class SchedulesBase : ISchedules
    {
        private readonly int _minutesInterval;
        protected JobExecutionControl job;
        protected string JobName;
        protected readonly ILogger _log;

        public SchedulesBase(ILoggerFactory log)
        {
            this._minutesInterval = 5;
            this._log = log.CreateLogger(this.GetType().Name);
        }

        protected void Start()
        {
            if (this.job.IsNotNull())
                this.job.Start(JobName);
        }

        protected void Stop()
        {
            if (this.job.IsNotNull())
                this.job.Stop(JobName);
        }

        protected bool IsRunning()
        {
            return this.job.IsNull() ? false : this.job.IsRuning(JobName);
        }

        protected abstract void Process();

        public virtual int GetMinutesInterval()
        {
            return this._minutesInterval;
        }

        [AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(timeoutInSeconds: 240)]
        public virtual void Execute()
        {
            try
            {
                if (this.IsRunning())
                    return;

                this.Start();
                this.Process();
            }
            catch (Exception ex)
            {
                this._log.LogError(ex.StackTrace);
                throw ex;
            }
            finally
            {
                this.Stop();
            }
        }
        
    }
}
