using HangFire.Interfaces;
using System.Collections.Generic;

namespace HangFire.Model
{
    public class SchedulesContainer : ISchedulesContainer
    {
        private readonly List<ISchedules> _schedules;

        public SchedulesContainer()
        {
            this._schedules = new List<ISchedules>();
        }

        public void Add(ISchedules schedules)
        {
            this._schedules.Add(schedules);

        }

        public IEnumerable<ISchedules> GetJobs()
        {
            return this._schedules;
        }
    }
}