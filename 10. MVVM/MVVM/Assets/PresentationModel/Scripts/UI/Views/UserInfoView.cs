using OTUSHW.MVVM.UI;
using OTUSHW.MVVM.UI.ViewModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PresentationModel.Scripts.UI.Views
{
    public sealed class UserInfoView : MonoBehaviour
    {
        [SerializeField] 
        private Image _avatarImage;

        [SerializeField] 
        private TMP_Text _nicknameText;

        [SerializeField]
        private TMP_Text _levelText;

        [SerializeField]
        private TMP_Text _descriptionText;

        private IUserInfoModel _userInfoModel;

        [Inject]
        private PlayerLevelManager _playerLevelManager;

        public void Show(IUserInfoModel userInfoModel)
        {
            _userInfoModel = userInfoModel;
            if (_userInfoModel != null)
            {
                Subscribe();
                UpdateView();
            }
        }

        private void Subscribe()
        {
            _playerLevelManager.OnLevelUp += UpdateLevelRender;
            _userInfoModel.OnAvatarChanged += UpdateAvatar;
            _userInfoModel.OnDescriptionChanged += UpdateDescription;
            _userInfoModel.OnNicknameChanged += UpdateNickname;
        }

        private void UpdateView()
        {
            UpdateAvatar(_userInfoModel.Avatar);
            UpdateNickname(_userInfoModel.Nickname);
            UpdateDescription(_userInfoModel.Description);
            UpdateLevelRender();
        }

        private void UpdateAvatar(Sprite sprite)
        {
            _avatarImage.sprite = sprite;
        }

        private void UpdateNickname(string nickaname)
        {
            _nicknameText.text = nickaname;
        }

        private void UpdateDescription(string description)
        {
            _descriptionText.text = description;
        }

        private void UpdateLevelRender()
        {
            _levelText.text = _userInfoModel.Level;
        }

        private void Unsubscribe()
        {
            _playerLevelManager.OnLevelUp -= UpdateLevelRender;
            _userInfoModel.OnAvatarChanged -= UpdateAvatar;
            _userInfoModel.OnDescriptionChanged -= UpdateDescription;
            _userInfoModel.OnNicknameChanged -= UpdateNickname;
        }

        private void OnDisable()
        {
            if(_userInfoModel != null && _playerLevelManager != null)
                Unsubscribe();
        }
    }
}