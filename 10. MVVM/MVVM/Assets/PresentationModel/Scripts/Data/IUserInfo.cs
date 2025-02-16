using System;
using UnityEngine;

namespace OTUSHW.MVVM.UI.Data
{
    public interface IUserInfo 
    {
        public Sprite Avatar { get; }
        public string Nickname { get; }
        public string Description { get; }
        public int CurrentXp { get; }
        public int CurrentLvl { get; }
        public CharacterInfo CharacterInfo { get; }
        
        public event Action<Sprite> OnAvatarChanged;
        public event Action<string> OnNicknameChanged;
        public event Action<string> OnDescriptionChanged;
    }
}
