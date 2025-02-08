using System;
using UnityEngine;

namespace Lessons.Architecture.PM.UI
{
    public interface IUserInfoPopupModel : IDisposable
    {
        public Sprite AvatarImg { get; }
        public string Nickname { get; }
        public string Level { get; } 
        public string Description { get; }
        public string XpProgress { get; } 
        public string MaxXpProgress { get; } 
        public float XpNormalizedValue { get; }
        public string XpValueString { get; }
        public bool IsLevelUpButtonInteractable { get; }
        public event Action<bool> OnBuyButtonIsInteractable;
        public event Action OnExpChanged;
        public event Action OnLevelChanged;
        public event Action OnCharacterStatsChanged;
        public Data.UserInfo UserInfo { get; }
        public void LevelUpBtnClicked();
    }
}