using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OTUSHW.MVVM.UI.Data
{
    [CreateAssetMenu(fileName = "UserInfo", menuName = "Data/New User Info")]
    public sealed class UserInfoSO : ScriptableObject, IUserInfo
    {
        [SerializeField]
        private Sprite _avatar;

        [SerializeField]
        private string _nickname;

        [SerializeField]
        private string _description;

        [SerializeField]
        private CharacterInfo _characterInfo = new();

        [SerializeField] 
        private int _currentXp;

        [SerializeField] 
        private int _currentLvl;

        public Sprite Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnAvatarChanged?.Invoke(_avatar);
            }
        }

        public string Nickname
        {
            get => _nickname;
            set
            {
                _nickname = value;
                OnNicknameChanged?.Invoke(_nickname);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnDescriptionChanged?.Invoke(_description);
            }
        }

        public int CurrentXp { get => _currentXp; set => _currentXp = value; }
        public int CurrentLvl { get => _currentLvl; set => _currentLvl = value; }
        public CharacterInfo CharacterInfo => _characterInfo;
        public event Action<Sprite> OnAvatarChanged;
        public event Action<string> OnNicknameChanged;
        public event Action<string> OnDescriptionChanged;
    }
}
