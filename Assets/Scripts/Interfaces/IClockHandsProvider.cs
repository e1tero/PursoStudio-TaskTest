using UnityEngine;

namespace CurrentTime_TestTask
{
    public interface IClockHandsProvider
    {
        Transform GetHourHand();
        Transform GetMinuteHand();
    }
}