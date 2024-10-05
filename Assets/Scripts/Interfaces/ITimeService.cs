using System;

namespace CurrentTime_TestTask
{
    public interface ITimeService
    {
        event Action OnTimeInitialized;
        DateTime GetCurrentTime();
        void SetCurrentTime(DateTime newTime);
        void SynchroniseTimeWithServer();
    }
}