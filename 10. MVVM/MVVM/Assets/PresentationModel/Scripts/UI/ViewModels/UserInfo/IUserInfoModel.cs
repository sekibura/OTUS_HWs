using System;
using UnityEngine;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public interface IUserInfoModel : IDisposable
    {
        public Sprite Avatar { get; }
        public string Nickname { get; }
        public string Level { get; } 
        public string Description { get; }

        public event Action<Sprite> OnAvatarChanged;
        public event Action<string> OnNicknameChanged;
        public event Action<string> OnLevelChanged;
        public event Action<string> OnDescriptionChanged;
        
        public Data.UserInfoSO UserInfoSo { get; }
    }
}