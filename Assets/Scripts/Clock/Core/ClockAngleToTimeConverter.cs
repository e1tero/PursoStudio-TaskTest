using System;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public class ClockAngleToTimeConverter
    {
        private readonly ITimeService _timeService;
        private readonly IClockHandsProvider _clockHandsProvider;

        public ClockAngleToTimeConverter(ITimeService timeService, IClockHandsProvider clockHandsProvider)
        {
            _timeService = timeService;
            _clockHandsProvider = clockHandsProvider;
        }

        public void UpdateTimeBasedOnHands(HandType handType, float angle)
        {
            DateTime currentTime = _timeService.GetCurrentTime();

            if (handType == HandType.Hour)
            {
                UpdateTimeFromHourHand(360 - angle, currentTime);
            }
            else if (handType == HandType.Minute)
            {
                UpdateTimeFromMinuteHand(360 - angle, currentTime);
            }
        }

        private void UpdateTimeFromHourHand(float hourAngle, DateTime currentTime)
        {
            int hours = Mathf.FloorToInt(hourAngle / 30f);
            float hourFraction = (hourAngle % 30) / 30f;
            int minutes = Mathf.FloorToInt(hourFraction * 60);

            int newHour = hours % 12;
            if (currentTime.Hour >= 12)
            {
                newHour += 12;
            }

            DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, newHour, minutes,
                currentTime.Second);
            _timeService.SetCurrentTime(newTime);

            float minuteAngle = minutes * 6f;
            _clockHandsProvider.GetMinuteHand().parent.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        }

        private void UpdateTimeFromMinuteHand(float minuteAngle, DateTime currentTime)
        {
            int minutes = Mathf.FloorToInt(minuteAngle / 6f);
            minutes = Mathf.Clamp(minutes, 0, 59);

            int previousMinutes = currentTime.Minute;
            int hoursChange = 0;

            if (previousMinutes > 45 && minutes < 15)
            {
                hoursChange = 1; 
            }
            else if (previousMinutes < 15 && minutes > 45)
            {
                hoursChange = -1;
            }

            int newHour = currentTime.Hour + hoursChange;
            newHour = ClockExtensions.NormalizeHour(newHour);

            try
            {
                DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                    newHour, minutes, currentTime.Second);
                _timeService.SetCurrentTime(newTime);

                float hourAngle = newHour % 12 * 30f + (minutes / 60f) * 30f;
                _clockHandsProvider.GetHourHand().parent.localRotation = Quaternion.Euler(0, 0, -hourAngle);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogError($"Ошибка при создании DateTime: {e.Message}. Часы: {newHour}, Минуты: {minutes}");
            }
        }
    }
}