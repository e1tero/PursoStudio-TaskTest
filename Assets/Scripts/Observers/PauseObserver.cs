using System;
using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask
{
    public class PauseObserver
    {
        public event Action OnPause;
        public event Action OnResume;
        
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