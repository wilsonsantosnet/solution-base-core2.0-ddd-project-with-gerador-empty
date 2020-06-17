using System.Collections.Generic;

namespace Seed.HangFire.Base
{
    public class JobExecutionControl
    {
        private Dictionary<string, bool> Running { get; set; }

        public JobExecutionControl()
        {
            this.Running = new Dictionary<string, bool>();
        }

        public bool IsRuning(string Job)
        {
            if (this.Running.ContainsKey(Job))
                return this.Running[Job];

            return false;
        }

        public void Start(string Job)
        {
            if (this.Running.ContainsKey(Job))
                this.Running[Job] = true;
            else {
                this.Running.Add(Job, true);
            }
        }

        public void Stop(string Job)
        {
            if(this.Running.ContainsKey(Job))
                this.Running[Job] = false;
        }

    }
}
