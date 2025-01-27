using ShootEmUp.Modules.Components;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField]
        private WeaponComponent _weaponComponent;

        [SerializeField]
        private HitPointsComponent _hitPointsComponent;

        [SerializeField]
        private MoveComponent _moveComponent;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CharacterController>().AsSingle()
                .WithArguments(_moveComponent, _weaponComponent, _hitPointsComponent);
        }
    }
}
