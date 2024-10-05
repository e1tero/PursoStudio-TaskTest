using UnityEngine;
using Zenject;

namespace CurrentTime_TestTask.Installers
{ 
    [CreateAssetMenu(fileName = "GameSystemInstaller", menuName = "Installers/New GameSystemInstaller", order = 1)]
    public class GameSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PauseObserver>()
                .AsSingle();
            
            Container
                .Bind<ITimeSource>()
                .To<TimeApiSource>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<TimeService>()
                .AsSingle();
            
            Container
                .Bind<ITickable>()
                .To<TimeSynchronizationManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeUpdater>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<UserTimeInputHandler>()
                .AsSingle();
        }
    }
}