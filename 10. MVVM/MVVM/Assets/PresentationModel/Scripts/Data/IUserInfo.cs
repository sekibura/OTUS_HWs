using UnityEngine;

namespace Lessons.Architecture.PM.Data
{
    public interface IUserInfo 
    {
        public Sprite AvatarImg { get; }
        public string Nickname { get; }
        public string Description { get; }

        public int CurrentXp { get; }

        public int CurrentLvl { get; }
        public CharacterInfo CharacterInfo { get; }
    }
}
