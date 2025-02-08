using System.Collections.Generic;
using Lessons.Architecture.PM.Helpers;
using Lessons.Architecture.PM.UI;
using ShootEmUp.Modules.Base;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Installers
{
    public class CommonInstaller : MonoInstaller
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
        private CharacterStatItemPresenter _characterStatItemPresenter;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerLevelManager>().AsSingle().NonLazy();
            Container.Bind<PlayerLevelHelper>().FromInstance(_playerLevelHelper).AsSingle();
            Container.Bind<UserHelper>().FromInstance(_userHelper).AsSingle();
            Container.Bind<UI.PopUpManager>().AsSingle().WithArguments(_popUps);
            Container.Bind<CharacterInfo>().AsSingle();
            Container.Bind<UserInfo>().AsSingle();

            Container.Bind<PopupUserInfo>().FromComponentInHierarchy().AsSingle();
            
            BindCharacterStatsItemStuff();
        }

        private void BindCharacterStatsItemStuff()
        {
            Container.Bind<IObjectPool<CharacterStatItemPresenter>>()
                .To<CharacterStatItemsPool>()
                .AsSingle()
                .WithArguments(20);
            Container.Bind<ObjectFactory<CharacterStatItemPresenter>>()
                .ToSelf()
                .AsSingle()
                .WithArguments(_characterStatItemPresenter.GetComponent<CharacterStatItemPresenter>(), _characterStatsRoot, Container);
        }
    }
}