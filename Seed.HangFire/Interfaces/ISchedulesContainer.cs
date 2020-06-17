using System.Collections.Generic;

namespace Seed.HangFire.Interfaces
{
    public interface ISchedulesContainer
    {
        void Add(ISchedules schedules);

        IEnumerable<ISchedules> GetJobs();
    }
}