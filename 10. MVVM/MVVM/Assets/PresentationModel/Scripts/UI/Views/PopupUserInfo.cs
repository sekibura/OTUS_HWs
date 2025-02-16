using System;
using System.Collections.Generic;
using OTUSHW.MVVM.UI.ViewModel;
using PresentationModel.Scripts.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace OTUSHW.MVVM.UI.View
{
    public sealed class PopupUserInfo : BasePopup
    {
        [SerializeField] 
        private UserInfoView _userInfoView;
        private IUserInfoModel _userInfoViewModel;

        [SerializeField] 
        private UserXPView _userXpView;
        private IUserXPModel _userXPModel;

        [SerializeField] 
        private Button _closeButton;

        [SerializeField]
        private CharacterStatsView _characterStatsView;
        private ICharacterStatsModel _characterStatsModel;

        private readonly List<IDisposable> _disposables = new();

        public void SetData(IUserInfoModel userInfoModel, IUserXPModel userXPModel,
            ICharacterStatsModel characterStatsModel)
        {
            _userInfoViewModel = userInfoModel;
            _userXPModel = userXPModel;
            _characterStatsModel = characterStatsModel;

            if (_userInfoViewModel != null && _userXPModel != null && _characterStatsModel != null)
            {
                _disposables.Add(_userInfoViewModel);
                _disposables.Add(_userXPModel);
                _disposables.Add(_characterStatsModel);

                Subscribe();

                _userXpView.Show(_userXPModel);
                _userInfoView.Show(_userInfoViewModel);
                _characterStatsView.Show(_characterStatsModel);
            }
        }

        private void Subscribe()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void Unsubscribe()
        {
            _closeButton.onClick.RemoveAllListeners();
        }

        public override void Hide()
        {
            base.Hide();

            if (_userInfoViewModel == null)
                return;

            Unsubscribe();
            Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            while (_disposables.Count > 0)
            {
                var a = _disposables[0];
                a.Dispose();
                _disposables.Remove(a);
            }
        }
    }
}
