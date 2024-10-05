using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeInputView : IInitializable, IDisposable
    {
        private readonly TMP_InputField _timeInputField;
        private readonly Button _setTimeButton;
        
        public event Action<string> OnSetTimeButtonClicked;

        public TimeInputView(TMP_InputField timeInputField, Button setTimeButton)
        {
            _timeInputField = timeInputField;
            _setTimeButton = setTimeButton;
        }

        public void Initialize()
        {
            _setTimeButton.onClick.AddListener(OnSetTimeButtonClickHandler);
            _timeInputField.onValueChanged.AddListener(OnTimeInputChangedHandler);
        }

        public void Dispose()
        {
            _setTimeButton.onClick.RemoveListener(OnSetTimeButtonClickHandler);
            _timeInputField.onValueChanged.RemoveListener(OnTimeInputChangedHandler);
        }

        private void OnTimeInputChangedHandler(string input)
        {
            _timeInputField.SetTextWithoutNotify(input.FormatToTime());
        }

        private void OnSetTimeButtonClickHandler()
        {
            OnSetTimeButtonClicked?.Invoke(_timeInputField.text);
        }
    }
}