using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CurrentTime_TestTask
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;
        [SerializeField] private Transform _secondHand;

        [SerializeField] private TimeService _timeService;
        private bool _isPaused = false;

        private void Start()
        {
            if (_timeService == null)
            {
                Debug.LogError("TimeService не найден на сцене.");
                return;
            }
            
            _timeService.OnTimeInitialized += SetClockHandsInstantly;
        }

        private IEnumerator UpdateClock()
        {
            while (!_isPaused)
            {
                UpdateClockHandsSmooth();
                yield return new WaitForSeconds(1);
            }
        }

        private void SetClockHandsInstantly()
        {
            DateTime currentTime = _timeService.GetCurrentTime();
            
            float seconds = currentTime.Second;
            float minutes = currentTime.Minute + seconds / 60f;
            float hours = currentTime.Hour % 12 + minutes / 60f;
            
            _secondHand.localRotation = Quaternion.Euler(0, 0, -seconds * 6f);
            _minuteHand.localRotation = Quaternion.Euler(0, 0, -minutes * 6f);
            _hourHand.localRotation = Quaternion.Euler(0, 0, -hours * 30f);
            
            StartCoroutine(UpdateClock());
        }
        
        private void UpdateClockHandsSmooth()
        {
            DateTime currentTime = _timeService.GetCurrentTime();
            
            float seconds = currentTime.Second;
            float minutes = currentTime.Minute + seconds / 60f;
            float hours = currentTime.Hour % 12 + minutes / 60f;
            
            _secondHand.DOLocalRotate(new Vector3(0, 0, -seconds * 6f), 1f)
                .SetEase(Ease.Linear); 
            _minuteHand.DOLocalRotate(new Vector3(0, 0, -minutes * 6f), 1f)
                .SetEase(Ease.Linear); 
            _hourHand.DOLocalRotate(new Vector3(0, 0, -hours * 30f), 1f)
                .SetEase(Ease.Linear);
        }

        public void PauseClock()
        {
            _isPaused = true;
            DOTween.PauseAll();
        }

        public void ResumeClock()
        {
            _isPaused = false;
            SetClockHandsInstantly();
        }
    }
}