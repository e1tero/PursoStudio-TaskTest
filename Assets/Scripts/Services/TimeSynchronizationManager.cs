using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeSynchronizationManager : ITickable, IInitializable
    {
        private readonly ITimeService _timeService;
        private float _nextUpdate;

        public TimeSynchronizationManager(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Initialize()
        {
            _nextUpdate = Time.time + 3600f; //1 hour
        }

        void ITickable.Tick()
        {
            if (Time.time >= _nextUpdate)
            {
                _timeService.SynchroniseTimeWithServer();
                _nextUpdate = Time.time + 3600f;
            }
        }
    }
}