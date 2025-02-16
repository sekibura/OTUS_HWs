using OTUSHW.MVVM.UI.View;
using OTUSHW.MVVM.UI.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace OTUSHW.MVVM.UI.Helpers
{
    public sealed class UserHelper : MonoBehaviour
    {
        [SerializeField] 
        private Data.UserInfoSO _userInfoSo;
        
        private PopUpManager _popupManager;
        private PlayerLevelManager _playerLevelManager;
        
        [Inject]
        private void Construct(PopUpManager popupManager, PlayerLevelManager playerLevelManager)
        {
            _popupManager = popupManager;
            _playerLevelManager = playerLevelManager;
        }

        [Button]
        private void ShowPopupUserInfo()
        {
            UserInfoModel userinfoModel = new UserInfoModel(_userInfoSo, _playerLevelManager);
            UserXPModel userXPModel = new UserXPModel(_userInfoSo, _playerLevelManager);
            CharacterStatsModel characterStatsModel = new CharacterStatsModel(userinfoModel.UserInfoSo.CharacterInfo);
            
            PopupUserInfo popUp = _popupManager.ShowPopUp<PopupUserInfo>();
            popUp.SetData(userinfoModel, userXPModel, characterStatsModel);
        }
    }
}
