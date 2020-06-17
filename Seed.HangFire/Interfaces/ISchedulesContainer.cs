using System.Collections.Generic;

namespace HangFire.Interfaces
{
    public interface ISchedulesContainer
    {
        void Add(ISchedules schedules);

        IEnumerable<ISchedules> GetJobs();
    }
}