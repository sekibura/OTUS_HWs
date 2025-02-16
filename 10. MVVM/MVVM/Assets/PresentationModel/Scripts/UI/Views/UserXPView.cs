using OTUSHW.MVVM.UI.ViewModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OTUSHW.MVVM.UI.View
{
    public sealed class UserXPView : MonoBehaviour
    {
        [SerializeField]
        private Image _xpProgressFiller;

        [SerializeField]
        private TMP_Text _xpText;

        [SerializeField]
        private Button _levelUpButton;

        private IUserXPModel _userXPModel;

        public void Show(IUserXPModel userXPModel)
        {
            _userXPModel = userXPModel;
            
            _levelUpButton.interactable = _userXPModel.IsLevelUpButtonInteractable;
            Subscribe();
            UpdateView();
        }

        private void Subscribe()
        {
            _levelUpButton.onClick.AddListener(BtnLevelUp);
        
            _userXPModel.OnBuyButtonIsInteractable += SetBuyButtonInteractable;
            _userXPModel.OnExpChanged += UpdateXpRender;
        }

        private void Unsubscribe()
        {
            _levelUpButton.onClick.RemoveAllListeners();
            _userXPModel.OnBuyButtonIsInteractable -= SetBuyButtonInteractable;
            _userXPModel.OnExpChanged -= UpdateXpRender;
        }

        private void UpdateView()
        {
            UpdateXpRender();
        }

        private void UpdateXpRender()
        {
            _xpText.text = _userXPModel.XpValueString;
            _xpProgressFiller.fillAmount = _userXPModel.XpNormalizedValue;
        }

        private void SetBuyButtonInteractable(bool isInteractable)
        {
            _levelUpButton.interactable = isInteractable;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void BtnLevelUp()
        {
            _userXPModel.LevelUpBtnClicked();
        }
    }
}