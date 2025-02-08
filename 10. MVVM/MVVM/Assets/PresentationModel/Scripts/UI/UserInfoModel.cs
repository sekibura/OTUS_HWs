using System;
using Lessons.Architecture.PM.UI;
using UnityEngine;

namespace Lessons.Architecture.PM
{
    public class UserInfoModel : IUserInfoPopupModel
    {
        public Sprite AvatarImg => UserInfo.AvatarImg;
        public string Nickname => UserInfo.Nickname;
        public string Level => _playerLevelManager.CurrentLevel.ToString();
        public string Description => UserInfo.Description;
        public string XpProgress => _playerLevelManager.CurrentExperience.ToString();
        public string MaxXpProgress => _playerLevelManager.RequiredExperience.ToString();
        public float XpNormalizedValue => (float)_playerLevelManager.CurrentExperience / _playerLevelManager.RequiredExperience;
        public string XpValueString => $"{XpProgress}/{MaxXpProgress}"; 
        public bool IsLevelUpButtonInteractable => _playerLevelManager.CanLevelUp();
        
        public event Action<bool> OnBuyButtonIsInteractable;
        public event Action OnExpChanged;
        public event Action OnLevelChanged;
        public event Action OnCharacterStatsChanged;
        public Data.UserInfo UserInfo { get; private set; }
        
        private PlayerLevelManager _playerLevelManager;

        public UserInfoModel(Data.UserInfo userInfo, PlayerLevelManager playerLevelManager)
        {
            this.UserInfo = userInfo;
            _playerLevelManager = playerLevelManager;
            _playerLevelManager.SetData(this.UserInfo.CurrentLvl, this.UserInfo.CurrentXp);
            Subscriptions();
        }

        private void Subscriptions()
        {
            _playerLevelManager.OnExperienceChanged += ExpChanged;
            _playerLevelManager.OnLevelUp += LevelUp;
            UserInfo.CharacterInfo.OnStatAdded += OnCharacterStatsAdded;
            UserInfo.CharacterInfo.OnStatRemoved += OnCharacterStatsRemoved;
            Debug.Log("Subscribed");
        }
        
        private void Unsubscriptions()
        {
            _playerLevelManager.OnExperienceChanged -= ExpChanged;
            _playerLevelManager.OnLevelUp -= LevelUp;
            UserInfo.CharacterInfo.OnStatAdded -= OnCharacterStatsAdded;
            UserInfo.CharacterInfo.OnStatRemoved -= OnCharacterStatsRemoved;
        }

        private void ExpChanged(int newValue)
        {
            OnExpChanged?.Invoke();
            UserInfo.CurrentXp = newValue;
            OnBuyButtonIsInteractable?.Invoke(_playerLevelManager.CanLevelUp());
        }

        private void LevelUp()
        {
            OnLevelChanged?.Invoke();
            UserInfo.CurrentLvl = _playerLevelManager.CurrentLevel;
            ExpChanged(_playerLevelManager.CurrentExperience);
        }
        
        public void LevelUpBtnClicked()
        {
            _playerLevelManager.LevelUp();
        }

        public void OnCharacterStatsAdded(CharacterStat characterStat)
        {
            OnCharacterStatsChanged?.Invoke();
        }
        public void OnCharacterStatsRemoved(CharacterStat characterStat)
        {
            OnCharacterStatsChanged?.Invoke();
        }

        public void Dispose()
        {
            Unsubscriptions();
        }
    }
}