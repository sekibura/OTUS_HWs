using System;
using OTUSHW.MVVM.UI.Data;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public interface IUserXPModel: IDisposable
    {
        public string XpProgress { get; } 
        public string MaxXpProgress { get; } 
        public float XpNormalizedValue { get; }
        public string XpValueString { get; }

        public bool IsLevelUpButtonInteractable { get; }

        public event Action<bool> OnBuyButtonIsInteractable;
        public event Action OnExpChanged;
        public event Action OnLevelChanged;

        public UserInfoSO UserInfoSo { get; }
        public void LevelUpBtnClicked();
    }
}