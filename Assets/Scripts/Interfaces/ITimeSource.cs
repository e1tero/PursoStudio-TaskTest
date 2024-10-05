using System;
using System.Collections;

namespace CurrentTime_TestTask
{
    public interface ITimeSource
    {
        IEnumerator GetCurrentTime(Action<DateTime> callback);
    }
}