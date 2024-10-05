using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask.Installers
{
    public class ClockHandsInstaller : MonoInstaller
    {
        [Header("Analog Clock hands")]
        [SerializeField] private Transform _hourHand;
        [SerializeField] private Transform _minuteHand;

        public override void InstallBindings()
        {
            Container
                .Bind<IClockHandsProvider>()
                .To<ClockHandsProvider>()
                .AsSingle()
                .WithArguments(_hourHand, _minuteHand);
        }
    }
}