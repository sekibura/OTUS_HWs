using Zenject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OTUSHW.MVVM.UI.Helpers
{
    public sealed class PlayerLevelHelper : MonoBehaviour
    {
        [Inject]
        private PlayerLevelManager _playerLevel;

        [Button]
        public void AddExp(int value)
        {
            _playerLevel.AddExperience(value);
        }

        [Button]
        private void Levelup()
        {
            _playerLevel.LevelUp();
        }
    }
}