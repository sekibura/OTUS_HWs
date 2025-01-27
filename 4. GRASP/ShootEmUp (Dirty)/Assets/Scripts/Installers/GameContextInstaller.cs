using ShootEmUp.Modules.Base;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameContextInstaller: MonoInstaller
    {
        [SerializeField]
        private LevelBackground _levelBackground;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<LevelBackground>().FromInstance(_levelBackground).AsSingle(); 
            Container.Bind<CoroutineRunner>().FromComponentInHierarchy().AsSingle(); 
        }
    }
}