using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask
{
    public class TimeControlUIHandler : IInitializable, IDisposable
    {
        private readonly Button _enterEditModeButton;
        private readonly Button _saveButton;
        private readonly PauseObserver _pauseObserver;
        private readonly ClockInputHandler _clockInputHandler;

        [Inject]
        public TimeControlUIHandler(Button enterEditModeButton, Button saveButton,ClockInputHandler clockInputHandler, PauseObserver pauseObserver)
        {
            _enterEditModeButton = enterEditModeButton;
            _saveButton = saveButton;
            _clockInputHandler = clockInputHandler;
            _pauseObserver = pauseObserver;
        }

        public void Initialize()
        {
            _enterEditModeButton.onClick.AddListener(OnEnterEditModeButtonClicked);
            _saveButton.onClick.AddListener(OnSaveButtonClicked);
        }

        public void Dispose()
        {
            _enterEditModeButton.onClick.RemoveListener(OnEnterEditModeButtonClicked);
            _saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        }

        private void OnEnterEditModeButtonClicked()
        {
            _clockInputHandler.EnterEditMode();
            _pauseObserver.Pause();
        }

        private void OnSaveButtonClicked()
        {
            _clockInputHandler.ExitEditMode();
            _pauseObserver.Resume();
        }
    }
}