using ShootEmUp.Modules.Base;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class EnemyInstaller : MonoInstaller
    {
        [Header("Enemy fields")]

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private Transform _enemyContainerTransform;

        [SerializeField] 
        private GameObject _playerGameObject;
        public override void InstallBindings()
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