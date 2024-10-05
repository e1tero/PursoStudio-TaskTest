using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Analog Clock hands pivots")]
        [SerializeField] private Transform _hourHandPivot;
        [SerializeField] private Transform _minuteHandPivot;
        [SerializeField] private Transform _secondHandPivot;
        
        [Header("Analog Clock hands")]
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;

        [Header("Digit Clock Text")]
        [SerializeField] private TMP_Text timeText;
        
        [Header("Background Image Settings")]
        [SerializeField] private Image _backgroundImage; 
        [SerializeField] private Color _pausedColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private float _changeDuration;
        
        [Header("UI")]
        [SerializeField] private TMP_InputField _timeInputField;
        [SerializeField] private Button _setTimeButton;
        [SerializeField] private Button _enterEditModeButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ClockView>()
                .AsSingle()
                .WithArguments(_hourHandPivot, _minuteHandPivot, _secondHandPivot);
            
            Container
                .BindInterfacesAndSelfTo<TimeDisplay>()
                .AsSingle()
                .WithArguments(timeText);
            
            Container
                .BindInterfacesAndSelfTo<BackgroundColorChanger>()
                .AsSingle()
                .WithArguments(_backgroundImage, _pausedColor, _defaultColor, _changeDuration);
            
            Container
                .BindInterfacesAndSelfTo<TimeInputView>()
                .AsSingle()
                .WithArguments(_timeInputField, _setTimeButton);
            
            Container
                .BindInterfacesAndSelfTo<TimeControlUIHandler>()
                .AsSingle()
                .WithArguments(_enterEditModeButton, _saveButton);
            
            Container.BindInterfacesAndSelfTo<ClockInputHandler>()
                .AsSingle()
                .WithArguments(_graphicRaycaster);
    
            Container
                .Bind<ClockAngleToTimeConverter>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ClockEditorManager>()
                .AsSingle();
            
            Container
                .Bind<IClockHandsProvider>()
                .To<ClockHandsProvider>()
                .AsSingle()
                .WithArguments(_hourHand, _minuteHand);
        }
    }
}