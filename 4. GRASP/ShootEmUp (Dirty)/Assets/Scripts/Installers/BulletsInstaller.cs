using ShootEmUp.Modules.Base;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
	public class BulletsInstaller : MonoInstaller
	{
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
	}
}