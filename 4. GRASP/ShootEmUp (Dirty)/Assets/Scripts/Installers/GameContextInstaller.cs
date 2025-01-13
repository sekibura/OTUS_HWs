using ShootEmUp.Modules.Input;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameContextInstaller: MonoInstaller
    {
        [SerializeField]
        private InputManager _inputManager;
        
        [SerializeField]
        private CharacterController _characterController;
        
        [SerializeField]
        private BulletSystem _bulletSystem;
        
        [SerializeField]
        private BulletsObjectPool _bulletsObjectPool;
        
        [SerializeField]
        private EnemyObjectPool _enemyObjectPool;

        [SerializeField]
        private EnemyManager _enemyManager;
        
        [SerializeField]
        private LevelBackground _levelBackground;


        public override void InstallBindings()
        {
            Container.Bind<EnemyObjectPool>().FromInstance(_enemyObjectPool).AsSingle();
            Container.Bind<BulletsObjectPool>().FromInstance(_bulletsObjectPool).AsSingle();
            Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<BulletSystem>().FromInstance(_bulletSystem).AsSingle();
            Container.Bind<EnemyManager>().FromInstance(_enemyManager).AsSingle();
            Container.Bind<LevelBackground>().FromInstance(_levelBackground).AsSingle();
        }
    }
}