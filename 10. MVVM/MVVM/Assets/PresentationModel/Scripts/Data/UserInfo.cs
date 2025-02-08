using UnityEngine;

namespace Lessons.Architecture.PM.Data
{
    [CreateAssetMenu(fileName = "UserInfo", menuName = "Data/New User Info")]
    public sealed class UserInfo : ScriptableObject, IUserInfo
    {
        [SerializeField]
        private Sprite _avatarImage;
        
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
        
        public Sprite AvatarImg => _avatarImage;
        public string Nickname => _nickname;
        public string Description => _description;
        public int CurrentXp { get => _currentXp; set => _currentXp = value; }
        public int CurrentLvl { get => _currentLvl; set => _currentLvl = value; }
        public CharacterInfo CharacterInfo => _characterInfo;
    }
}
