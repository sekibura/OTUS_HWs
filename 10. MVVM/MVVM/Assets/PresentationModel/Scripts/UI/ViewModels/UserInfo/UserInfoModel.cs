using System;
using UnityEngine;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public sealed class UserInfoModel : IUserInfoModel
    {
        public Sprite Avatar => UserInfoSo.Avatar;
        public string Nickname => UserInfoSo.Nickname;
        public string Level => _playerLevelManager.CurrentLevel.ToString();
        public string Description => UserInfoSo.Description;

        public event Action<Sprite> OnAvatarChanged;
        public event Action<string> OnNicknameChanged;
        public event Action<string> OnLevelChanged;
        public event Action<string> OnDescriptionChanged;

        public Data.UserInfoSO UserInfoSo { get; private set; }
        
        private PlayerLevelManager _playerLevelManager;

        public UserInfoModel(Data.UserInfoSO userInfoSo, PlayerLevelManager playerLevelManager)
        {
            UserInfoSo = userInfoSo;
            _playerLevelManager = playerLevelManager;
            
            if (UserInfoSo != null && _playerLevelManager != null)
            {
                _playerLevelManager.SetData(this.UserInfoSo.CurrentLvl, this.UserInfoSo.CurrentXp);
                Subscribe();
            }
        }

        private void Subscribe()
        {
            UserInfoSo.OnAvatarChanged += AvatarChanged;
            UserInfoSo.OnNicknameChanged += NicknameChanged;
            UserInfoSo.OnDescriptionChanged += DescriptionChanged;
        }

        private void Unsubscribe()
        {
            UserInfoSo.OnAvatarChanged -= AvatarChanged;
            UserInfoSo.OnNicknameChanged -= NicknameChanged;
            UserInfoSo.OnDescriptionChanged -= DescriptionChanged;
        }

        private void AvatarChanged(Sprite avatar)
        {
            OnAvatarChanged?.Invoke(avatar);
        }

        private void DescriptionChanged(string description)
        {
            OnDescriptionChanged(description);
        }

        private void NicknameChanged(string nickname)
        {
            OnNicknameChanged?.Invoke(nickname);
        }

        public void Dispose()
        {
            Unsubscribe();
        }
    }
}