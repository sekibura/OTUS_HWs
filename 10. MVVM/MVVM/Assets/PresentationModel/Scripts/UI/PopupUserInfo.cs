using System;
using System.Collections.Generic;
using Lessons.Architecture.PM.UI;
using ShootEmUp.Modules.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class PopupUserInfo : BasePopup
{
    [SerializeField] 
    private Image _avatarImg;

    [SerializeField] 
    private TMP_Text _nicknameText;
    
    [SerializeField]
    private TMP_Text _levelText;
    
    [SerializeField]
    private TMP_Text _descriptionText;
    
    [SerializeField]
    private Image _xpProgressImg;
    
    [SerializeField]
    private TMP_Text _xpText;
    
    [SerializeField]
    private Button _levelUpButton;
    
    [SerializeField]
    private Button _closeButton;

    [SerializeField] 
    private Transform _characterStatsRoot;
    
    private IUserInfoPopupModel _userInfoPopupViewModel;
    
    private List<IDisposable> _disposables = new();
    
    [Inject]
    private IObjectPool<CharacterStatItemPresenter> _characterStatItemsPool;

    private List<CharacterStatItemPresenter> _activeItems = new();
    
    public override void Show(object data = null)
    {
        base.Show();
        
        _userInfoPopupViewModel = data as IUserInfoPopupModel;
        
        if (_userInfoPopupViewModel != null)
        {
            _disposables.Add(_userInfoPopupViewModel);
            Subscribe();
            
            _avatarImg.sprite = _userInfoPopupViewModel.AvatarImg;
            _nicknameText.text = _userInfoPopupViewModel.Nickname;
            _descriptionText.text = _userInfoPopupViewModel.Description;
            _levelUpButton.interactable = _userInfoPopupViewModel.IsLevelUpButtonInteractable;

            UpdateXpRender();
            UpdateLevelRender();
            UpdateCharacterStatRender();
        }
    }

    private void Subscribe()
    {
        _closeButton.onClick.AddListener(Hide);
        _levelUpButton.onClick.AddListener(BtnLevelUp);
        
        _userInfoPopupViewModel.OnBuyButtonIsInteractable += SetBuyButtonInteractable;
        _userInfoPopupViewModel.OnExpChanged += UpdateXpRender;
        _userInfoPopupViewModel.OnLevelChanged += UpdateLevelRender;
        _userInfoPopupViewModel.OnCharacterStatsChanged += UpdateCharacterStatRender;
    }

    private void Unsubscribe()
    {
        _levelUpButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
        
        _userInfoPopupViewModel.OnBuyButtonIsInteractable -= SetBuyButtonInteractable;
        _userInfoPopupViewModel.OnExpChanged -= UpdateXpRender;
        _userInfoPopupViewModel.OnLevelChanged -= UpdateLevelRender;
        _userInfoPopupViewModel.OnCharacterStatsChanged -= UpdateCharacterStatRender;
    }

    private void UpdateXpRender()
    {
        _xpText.text = _userInfoPopupViewModel.XpValueString;
        _xpProgressImg.fillAmount = _userInfoPopupViewModel.XpNormalizedValue;
    }

    private void UpdateLevelRender()
    {
        _levelText.text = _userInfoPopupViewModel.Level;
    }
    private void BtnLevelUp()
    {
        _userInfoPopupViewModel.LevelUpBtnClicked();
    }

    private void SetBuyButtonInteractable(bool isInteractable)
    {
        _levelUpButton.interactable = isInteractable;
    }

    private void UpdateCharacterStatRender()
    {
        while (_activeItems.Count > 0)
        {
            _characterStatItemsPool.ReturnToPool(_activeItems[0]);
            _activeItems.Remove(_activeItems[0]);
        }
        
        var stats = _userInfoPopupViewModel.UserInfo.CharacterInfo.GetAllStats();
        foreach (var stat in stats)
        {
            var statItem = _characterStatItemsPool.Get();
            statItem.Set(stat);
            statItem.transform.SetParent(_characterStatsRoot);
            statItem.transform.localScale = Vector3.one;
            _activeItems.Add(statItem);
            
        }
    }

    public override void Hide()
    {
        base.Hide();
        
        if(_userInfoPopupViewModel == null)
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
