using UnityEngine;

namespace CurrentTime_TestTask
{
    public class ClockHandsProvider : IClockHandsProvider
    {
        private readonly Transform _hourHand;
        private readonly Transform _minuteHand;

        public ClockHandsProvider(Transform hourHand, Transform minuteHand)
        {
            _hourHand = hourHand;
            _minuteHand = minuteHand;
        }

        public Transform GetHourHand()
        {
            return _hourHand;
        }

        public Transform GetMinuteHand()
        {
            return _minuteHand;
        }
    }
}