using ShootEmUp.Modules.Base;
using ShootEmUp.Modules.Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameContextInstaller: MonoInstaller
    {
        [SerializeField]
        private CharacterController _characterController;

        [SerializeField]
        private LevelBackground _levelBackground;
        
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<LevelBackground>().FromInstance(_levelBackground).AsSingle(); 
            Container.Bind<CoroutineRunner>().FromComponentInHierarchy().AsSingle(); 
        }
    }
}