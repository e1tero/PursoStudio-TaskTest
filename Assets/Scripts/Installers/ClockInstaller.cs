using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CurrentTime_TestTask.Installers
{
    public class ClockInstaller : MonoInstaller
    {
        [Header("Analog Clock hands pivots")]
        [SerializeField] private Transform _hourHandPivot;
        [SerializeField] private Transform _minuteHandPivot;
        [SerializeField] private Transform _secondHandPivot;

        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ClockView>()
                .AsSingle()
                .WithArguments(_hourHandPivot, _minuteHandPivot, _secondHandPivot);
        
            Container
                .BindInterfacesAndSelfTo<ClockInputHandler>()
                .AsSingle()
                .WithArguments(_graphicRaycaster);
    
            Container
                .Bind<ClockAngleToTimeConverter>()
                .AsSingle();
        
            Container
                .BindInterfacesAndSelfTo<ClockEditorManager>()
                .AsSingle();
        }
    }
}