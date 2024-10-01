using System;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public class TimeServiceManager : MonoBehaviour
    {
        public event Action OnPause;
        public event Action OnResume;

        [Header("Services")]
        [SerializeField] private TimeService _timeService;
        [SerializeField] private Clock _clock;
        [SerializeField] private TimeDisplay _timeDisplay;
        [SerializeField] private BackgroundColorChanger _backgroundColorChanger; 
        [SerializeField] private SetTimeFromInput _setTimeFromInput;

        private void Start()
        {
            ITimeSource timeSource = new TimeApiSource();
            _timeService.Initialize(timeSource);
            InitializeSubscribers();
        }

        private void InitializeSubscribers()
        {
            if (_clock != null)
            {
                OnPause += _clock.PauseClock;
                OnResume += _clock.ResumeClock;
            }

            if (_timeDisplay != null)
            {
                OnPause += _timeDisplay.PauseTimeDisplay;
                OnResume += _timeDisplay.ResumeTimeDisplay;
            }

            if (_backgroundColorChanger != null)
            {
                OnPause += _backgroundColorChanger.ChangeToPausedColor;
                OnResume += _backgroundColorChanger.ChangeToDefaultColor;
            }
            
            if (_setTimeFromInput != null)
            {
                OnPause += _setTimeFromInput.ShowInputField;
                OnResume += _setTimeFromInput.HideInputField;
            }
        }

        public void Pause()
        {
            OnPause?.Invoke();
        }

        public void Resume()
        {
            OnResume?.Invoke();
        }
    }
}