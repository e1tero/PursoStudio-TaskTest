using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [Header("Time Input View")]
        [SerializeField] private TMP_InputField _timeInputField;
        [SerializeField] private Button _saveButton;

        [Header("Time Control UI")]
        [SerializeField] private Button _enterEditModeButton;
        [SerializeField] private Button _saveButtonControl;

        [Header("Background Image Settings")]
        [SerializeField] private Image _backgroundImage; 
        [SerializeField] private Color _pausedColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private float _changeDuration;

        [Header("Time Display")]
        [SerializeField] private TMP_Text _timeDisplayText;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<TimeInputView>()
                .AsSingle()
                .WithArguments(_timeInputField, _saveButton);

            Container
                .BindInterfacesAndSelfTo<TimeControlUIHandler>()
                .AsSingle()
                .WithArguments(_enterEditModeButton, _saveButton);

            Container
                .BindInterfacesAndSelfTo<BackgroundColorChanger>()
                .AsSingle()
                .WithArguments(_backgroundImage, _pausedColor, _defaultColor, _changeDuration);
            
            Container
                .BindInterfacesAndSelfTo<TimeDisplay>()
                .AsSingle()
                .WithArguments(_timeDisplayText);
        }
    }
}