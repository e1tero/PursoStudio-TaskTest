using System;
using UnityEngine;
using UnityEngine.UI;

namespace CurrentTime_TestTask
{
    public class ClockEditorUI : MonoBehaviour
    {
        [SerializeField] private Button enterEditModeButton;
        [SerializeField] private Button saveButton;
        [SerializeField] private TimeServiceManager _timeServiceManager;
        [SerializeField] private ClockEditor _clockEditor;
        
        private void Start()
        {
            enterEditModeButton.onClick.AddListener(EnterEditMode);
            saveButton.onClick.AddListener(SaveEditedTime);
        }

        private void EnterEditMode()
        {
            _clockEditor.EnterEditMode();
            _timeServiceManager.Pause();
        }

        private void SaveEditedTime()
        {
            _clockEditor.SaveEditedTime();
            _timeServiceManager.Resume();
        }
    }
}