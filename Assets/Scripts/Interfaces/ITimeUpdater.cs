using System;

namespace CurrentTime_TestTask
{
    public interface ITimeUpdater
    {
        event Action<float, float, float, bool> OnTimeUpdated;
    }
}