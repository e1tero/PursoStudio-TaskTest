using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeDisplay : IInitializable, IDisposable
    {
        private readonly TMP_Text _timeText;
        private readonly ITimeUpdater _timeUpdater;

        [Inject]
        public TimeDisplay(TMP_Text timeText, ITimeUpdater timeUpdater)
        {
            _timeText = timeText;
            _timeUpdater = timeUpdater;
        }

        public void Initialize()
        {
            _timeUpdater.OnTimeUpdated += UpdateTimeText;
        }

        public void Dispose()
        {
            _timeUpdater.OnTimeUpdated -= UpdateTimeText;
        }

        private void UpdateTimeText(float hours, float minutes, float seconds, bool _)
        {
            _timeText.text = $"{(int)hours:D2}:{(int)minutes:D2}:{(int)seconds:D2}";
        }
    }
}