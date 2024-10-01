using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CurrentTime_TestTask
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private Text _timeText;
        [SerializeField] private TimeService _timeService;
        private Coroutine _updateCoroutine;

        private void Start()
        {
            if (_timeService == null)
            {
                Debug.LogError("TimeService не найден на сцене.");
                return;
            }
            
            ResumeTimeDisplay();
        }
        
        private IEnumerator UpdateTimeDisplay()
        {
            while (true)
            {
                UpdateTimeText();
                yield return new WaitForSeconds(1);
            }
        }
        
        private void UpdateTimeText()
        {
            DateTime currentTime = _timeService.GetCurrentTime();
            _timeText.text = currentTime.ToString("HH:mm:ss");
        }
        
        public void PauseTimeDisplay()
        {
            if (_updateCoroutine != null)
            {
                StopCoroutine(_updateCoroutine);
                _updateCoroutine = null;
            }
            
            _timeText.gameObject.SetActive(false);
        }
        
        public void ResumeTimeDisplay()
        {
            if (_updateCoroutine == null)
            {
                _updateCoroutine = StartCoroutine(UpdateTimeDisplay());
            }
            
            _timeText.gameObject.SetActive(true);
        }
    }
}