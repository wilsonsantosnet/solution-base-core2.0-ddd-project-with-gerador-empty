namespace Seed.HangFire.Interfaces
{

    public interface ISchedules
    {
        void Execute();

        int GetMinutesInterval();
    }

    public interface ISchedules<T> : ISchedules where T : class
    {

    }
}
