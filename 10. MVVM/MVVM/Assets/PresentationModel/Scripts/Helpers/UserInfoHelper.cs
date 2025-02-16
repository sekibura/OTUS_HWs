using OTUSHW.MVVM.UI.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OTUSHW.MVVM.UI.Helpers
{
    public sealed class UserInfoHelper : MonoBehaviour
    {
        [SerializeField]
        private UserInfoSO _userInfoSo;

        [Button]
        private void SetPicture(Sprite sprite)
        {
            _userInfoSo.Avatar = sprite;
        }

        [Button]
        private void SetNickname(string nickname)
        {
            _userInfoSo.Nickname = nickname;
        }

        [Button]
        private void SetDescription(string description)
        {
            _userInfoSo.Description = description;
        }
    }
}