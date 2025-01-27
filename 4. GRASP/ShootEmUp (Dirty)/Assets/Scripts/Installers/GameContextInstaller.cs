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
        private EnemyManager _enemyManager;
        
        [SerializeField]
        private LevelBackground _levelBackground;

        [Header("Enemy fields")]
        
        [SerializeField]
        private GameObject _enemyPrefab;
        
        [SerializeField]
        private Transform _enemyContainerTransform;

        [SerializeField] 
        private GameObject _targetCharacter;
        
        [Header("Bullet fields")]
        [SerializeField]
        private GameObject _bulletPrefab;
        
        [SerializeField]
        private Transform _bulletContainerTransform;
        
        [SerializeField]
        private BulletConfig _characterBulletConfig;
        
        [SerializeField]
        private BulletConfig _enemyBulletConfig;


        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<LevelBackground>().FromInstance(_levelBackground).AsSingle(); 
            Container.Bind<CoroutineRunner>().FromComponentInHierarchy().AsSingle(); 
            
            BindBulletSystem();
            BindEnemySystem();
        }

        private void BindBulletSystem()
        {
            Container.Bind<IObjectPool<Bullet>>()
                .To<BulletsObjectPool>()
                .AsSingle()
                .WithArguments(10);
            Container.Bind<ObjectFactory<Bullet>>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_bulletPrefab.GetComponent<Bullet>(), _bulletContainerTransform, Container);
            
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle();
            
            Container.Bind<BulletConfig>().WithId("CharacterBullet").FromInstance(_characterBulletConfig).AsCached();
            Container.Bind<BulletConfig>().WithId("EnemyBullet").FromInstance(_enemyBulletConfig).AsCached();
        }

        private void BindEnemySystem()
        {
            Container.Bind<IObjectPool<Enemy>>()
                .To<EnemyObjectPool>()
                .AsSingle()
                .WithArguments(6);
            Container.Bind<ObjectFactory<Enemy>>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_enemyPrefab.GetComponent<Enemy>(), _enemyContainerTransform, Container); 
            
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle().WithArguments(6);

            Container.Bind<EnemyPositions>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameObject>()
                .WithId("CharacterTarget")
                .FromInstance(_targetCharacter).AsSingle(); 

            
        }
    }
}