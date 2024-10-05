using System;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class UserTimeInputHandler : IInitializable
    {
        private readonly TimeInputView _timeInputView;
        private readonly ITimeService _timeService;
        
        public UserTimeInputHandler(TimeInputView timeInputView, ITimeService timeService)
        {
            _timeInputView = timeInputView;
            _timeService = timeService;
        }

        public void Initialize()
        {
            _timeInputView.OnSetTimeButtonClicked += OnSetTimeButtonClickedHandler;
        }

        private void OnSetTimeButtonClickedHandler(string inputTime)
        {
            if (string.IsNullOrWhiteSpace(inputTime))
            {
                return;
            }

            if (DateTime.TryParseExact(inputTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
            {
                var currentTime = _timeService.GetCurrentTime();
                DateTime newTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day,
                    parsedTime.Hour, parsedTime.Minute, currentTime.Second);
                _timeService.SetCurrentTime(newTime);
            }
            else
            {
                Debug.LogWarning("Неверный формат времени. Используйте формат HH:mm.");
            }
        }
    }
}