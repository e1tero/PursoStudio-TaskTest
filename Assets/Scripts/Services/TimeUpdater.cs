using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeUpdater : IInitializable, ITimeUpdater, IPauseListener
    {
        public event Action<float, float, float, bool> OnTimeUpdated;
        
        private readonly ITimeService _timeService;
        private readonly PauseObserver _pauseObserver;
        private bool _isPaused;
        
        public TimeUpdater(PauseObserver pauseObserver, ITimeService timeService)
        {
            _timeService = timeService;
            _pauseObserver = pauseObserver;
        }

        public void Initialize()
        {
            _timeService.OnTimeInitialized += () => UpdateCurrentTime(true);
            _pauseObserver.OnPause += OnPause;
            _pauseObserver.OnResume += OnResume;
            
            CoroutineRunner.Instance.StartCoroutine(UpdateCurrentTimeCoroutine());
        }

        private IEnumerator UpdateCurrentTimeCoroutine()
        {
            while (!_isPaused)
            {
                UpdateCurrentTime(false);
                yield return new WaitForSeconds(1);
            }
        }
        private void UpdateCurrentTime(bool isInstantly)
        {
            DateTime currentTime = _timeService.GetCurrentTime();
            float seconds = currentTime.Second;
            float minutes = currentTime.Minute + seconds / 60f;
            float hours = currentTime.Hour % 12 + minutes / 60f;
            OnTimeUpdated?.Invoke(hours, minutes, seconds,isInstantly);
        }
        
        
        public void OnPause()
        {
            _isPaused = true;
        }

        public void OnResume()
        {
            _isPaused = false;
            CoroutineRunner.Instance.StartCoroutine(UpdateCurrentTimeCoroutine());
        }
    }
}