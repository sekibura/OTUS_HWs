using System.Collections.Generic;
using OTUSHW.MVVM.UI.Helpers;
using OTUSHW.MVVM.UI.View;
using Sekibura.Modules.Base;
using UnityEngine;
using Zenject;

namespace OTUSHW.MVVM.UI.Installers
{
    public sealed class CommonInstaller : MonoInstaller
    {
        [SerializeField]
        private List<BasePopup> _popUps;

        [SerializeField]
        private PlayerLevelHelper _playerLevelHelper;

        [SerializeField]
        private UserHelper _userHelper;

        [SerializeField] 
        private Transform _characterStatsRoot;

        [SerializeField]
        private CharacterStatView _characterStatView;

        public override void InstallBindings()
        {
            Container.Bind<PlayerLevelManager>().AsSingle().NonLazy();
            Container.Bind<PlayerLevelHelper>().FromInstance(_playerLevelHelper).AsSingle();
            Container.Bind<UserHelper>().FromInstance(_userHelper).AsSingle();
            Container.Bind<PopUpManager>().AsSingle().WithArguments(_popUps);
            Container.Bind<CharacterInfo>().AsSingle();
            Container.Bind<PopupUserInfo>().FromComponentInHierarchy().AsSingle();

            BindCharacterStatsItemStuff();
        }

        private void BindCharacterStatsItemStuff()
        {
            Container.Bind<IObjectPool<CharacterStatView>>()
                .To<CharacterStatViewPool>()
                .AsSingle()
                .WithArguments(20);

            Container.Bind<ObjectFactory<CharacterStatView>>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_characterStatView.GetComponent<CharacterStatView>(), _characterStatsRoot, Container);
        }
    }
}