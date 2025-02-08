using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Helpers
{
    public class UserHelper : MonoBehaviour
    {
        [SerializeField] 
        private Data.UserInfo _userInfo;
        
        private UI.PopUpManager _popupManager;
        private PlayerLevelManager _playerLevelManager;

        
        [Inject]
        private void Construct(UI.PopUpManager popupManager, PlayerLevelManager playerLevelManager)
        {
            _popupManager = popupManager;
            _playerLevelManager = playerLevelManager;
        }

        [Button]
        private void ShowPopupUserInfo()
        {
            var userinfoModel = new UserInfoModel(_userInfo, _playerLevelManager);
            _popupManager.ShowPopUp<PopupUserInfo>(userinfoModel);
        }
    }
}
