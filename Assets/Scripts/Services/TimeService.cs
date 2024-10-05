using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeService : ITimeService, ITickable, IInitializable
    { 
        public event Action OnTimeInitialized;
        private readonly ITimeSource _timeSource;
        private DateTime _currentTime;

        public TimeService(ITimeSource timeSource)
        {
            _timeSource = timeSource;
        }

        public void Initialize()
        {
            SynchroniseTimeWithServer();
        }

        private IEnumerator GetTimeFromApi()
        {
            bool isDone = false;
            yield return CoroutineRunner.Instance.StartCoroutine(_timeSource.GetCurrentTime((DateTime time) =>
            {
                _currentTime = time != DateTime.MinValue ? time : DateTime.Now;
                isDone = true;
                OnTimeInitialized?.Invoke();
            }));

            while (!isDone)
                yield return null;
        }

        public void SynchroniseTimeWithServer()
        {
            CoroutineRunner.Instance.StartRunnerCoroutine(GetTimeFromApi());
        }

        void ITickable.Tick()
        {
            _currentTime = _currentTime.AddSeconds(Time.deltaTime);
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