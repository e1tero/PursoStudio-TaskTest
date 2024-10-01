using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CurrentTime_TestTask
{
    public class SetTimeFromInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _timeInputField; 
        [SerializeField] private Button _setTimeButton;
        [SerializeField] private TimeService _timeService;
    
        private void Start()
        {
            _setTimeButton.onClick.AddListener(OnSetTimeButtonClick);
            _timeInputField.onValueChanged.AddListener(OnTimeInputChanged);
            
            HideInputField(); 
        }
        
        private void OnTimeInputChanged(string input)
        {
            _timeInputField.SetTextWithoutNotify(input.FormatToTime());
        }

        private void OnSetTimeButtonClick()
        {
            string inputTime = _timeInputField.text;
            
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

        public void ShowInputField()
        {
            _timeInputField.text = string.Empty;
            _timeInputField.gameObject.SetActive(true);
        }

        public void HideInputField()
        {
            _timeInputField.gameObject.SetActive(false);
        }
    }
}