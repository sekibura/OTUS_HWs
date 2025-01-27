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
        
        
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<InputManager>().AsSingle().NonLazy();
            Container.Bind<CharacterController>().FromInstance(_characterController).AsSingle();
            Container.Bind<LevelBackground>().FromInstance(_levelBackground).AsSingle(); 
            Container.Bind<CoroutineRunner>().FromComponentInHierarchy().AsSingle(); 
            
            BindEnemySystem();
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
                .WithId("PlayerGameObject")
                .FromInstance(_playerGameObject).AsSingle(); 

            
        }
    }
}