using System;
using OTUSHW.MVVM.UI.Data;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public sealed class UserXPModel : IUserXPModel
    {
        public string XpProgress => _playerLevelManager.CurrentExperience.ToString();
        public string MaxXpProgress => _playerLevelManager.RequiredExperience.ToString();
        public float XpNormalizedValue => (float)_playerLevelManager.CurrentExperience / _playerLevelManager.RequiredExperience;
        public string XpValueString => $"{XpProgress}/{MaxXpProgress}";
        public bool IsLevelUpButtonInteractable => _playerLevelManager.CanLevelUp();
        public UserInfoSO UserInfoSo  { get; private set; }
        private PlayerLevelManager _playerLevelManager;
        
        public event Action<bool> OnBuyButtonIsInteractable;
        public event Action OnExpChanged;
        public event Action OnLevelChanged;

        public UserXPModel(UserInfoSO userInfoSo, PlayerLevelManager playerLevelManager)
        {
            UserInfoSo = userInfoSo;
            _playerLevelManager = playerLevelManager;
            Subscribe();
        }

        private void Subscribe()
        {
            _playerLevelManager.OnExperienceChanged += ExpChanged;
            _playerLevelManager.OnLevelUp += LevelUp;
        }

        private void Unsubscribe()
        {
            _playerLevelManager.OnExperienceChanged -= ExpChanged;
            _playerLevelManager.OnLevelUp -= LevelUp;
        }

        private void ExpChanged(int newValue)
        {
            OnExpChanged?.Invoke();
            UserInfoSo.CurrentXp = newValue;
            OnBuyButtonIsInteractable?.Invoke(_playerLevelManager.CanLevelUp());
        }

        private void LevelUp()
        {
            OnLevelChanged?.Invoke();
            UserInfoSo.CurrentLvl = _playerLevelManager.CurrentLevel;
            ExpChanged(_playerLevelManager.CurrentExperience);
        }

        public void LevelUpBtnClicked()
        {
            _playerLevelManager.LevelUp();
        }
        
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}