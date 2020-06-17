using Hangfire;
using HangFire.Base;
using HangFire.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HangFire.Jobs
{

    public class SampleJob : SchedulesBase, ISchedules<SampleJob>
    {
        public SampleJob(JobExecutionControl jobExecutionControl, ILoggerFactory log) : base(log)
        {
            this.job = jobExecutionControl;
            this.JobName = this.GetType().Name;
        }

        public override int GetMinutesInterval()
        {
            return 1440;
        }

        protected override void Process()
        {
            this.ExecuteAsync().Wait();
        }

        [AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(timeoutInSeconds: 0)]
        public async Task ExecuteAsync()
        {

        }

    }

}
