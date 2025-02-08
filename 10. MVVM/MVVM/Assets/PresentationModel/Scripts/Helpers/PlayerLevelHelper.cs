using System;
using Zenject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM.Helpers
{
    public sealed class PlayerLevelHelper : MonoBehaviour
    {
        [Inject]
        private PlayerLevelManager _playerLevel;
        
        [SerializeField, ReadOnly]
        private float _experience;

        private void Start()
        {
            _experience = _playerLevel.CurrentExperience;
        }

        [Button]
        public void AddExp(int value)
        {
            _playerLevel.AddExperience(value);
            _experience = _playerLevel.CurrentExperience;
        }

        [Button]
        private void Levelup()
        {
            _playerLevel.LevelUp();
        }
    }
}