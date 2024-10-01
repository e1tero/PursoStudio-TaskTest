using System;
using System.Collections;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public class TimeService : MonoBehaviour
    {
        public event Action OnTimeInitialized;
        
        private ITimeSource _timeSource;
        private DateTime _currentTime;
        private float _nextUpdate;
        private bool _isPaused = false;
        
        public void Initialize(ITimeSource timeSource)
        {
            _timeSource = timeSource;
            StartCoroutine(InitializeTime());
        }

        private IEnumerator InitializeTime()
        {
            bool isDone = false;
            yield return StartCoroutine(_timeSource.GetCurrentTime((DateTime time) =>
            {
                if (time != DateTime.MinValue)
                {
                    _currentTime = time;
                }
                else
                {
                    _currentTime = DateTime.Now;
                }

                _nextUpdate = Time.time + 3600f;
                isDone = true;
                OnTimeInitialized?.Invoke();
            }));

            while (!isDone)
                yield return null;
        }

        private void Update()
        {
            if (!_isPaused)
            {
                _currentTime = _currentTime.AddSeconds(Time.deltaTime);
            }
            
            if (Time.time >= _nextUpdate)
            {
                StartCoroutine(CheckAndUpdateTime());
                _nextUpdate = Time.time + 3600f;
            }
        }

        private IEnumerator CheckAndUpdateTime()
        {
            DateTime serverTime = DateTime.MinValue;
            yield return StartCoroutine(_timeSource.GetCurrentTime((DateTime time) =>
            {
                if (time != DateTime.MinValue)
                {
                    serverTime = time;
                }
            }));

            if (serverTime != DateTime.MinValue)
            {
                if (Math.Abs((serverTime - _currentTime).TotalMinutes) > 1)
                {
                    Debug.Log("Коррекция времени с сервера");
                    _currentTime = serverTime;
                }
            }
        }
        
        public DateTime GetCurrentTime()
        {
            return _currentTime;
        }

        public void SetCurrentTime(DateTime newTime)
        {
            _currentTime = newTime;
        }
    }
}