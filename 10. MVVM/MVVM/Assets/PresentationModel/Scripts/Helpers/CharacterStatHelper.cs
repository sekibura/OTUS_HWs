using System.Linq;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Architecture.PM.Helpers
{
    public sealed class CharacterStatHelper : MonoBehaviour
    {
        [SerializeField]
        private Data.UserInfo _userInfo;

        [Button]
        public void AddStat(string name, int value)
        {
            _userInfo.CharacterInfo.AddStat(new CharacterStat(name, value));
        }

        [Button]
        public void RemoveStat(string name)
        {
            _userInfo.CharacterInfo.RemoveStat(name);
        }

        [Button]
        private void PrintAllStats()
        {
            var stats = _userInfo.CharacterInfo.GetAllStats();
            StringBuilder sb = new StringBuilder();
            foreach (var VARIABLE in stats)
            {
                sb.Append($"{VARIABLE.ToString()} ");
            }
            Debug.Log(sb.ToString());
        }
    
        [Button]
        public void SetDefaultStats()
        {
            AddStat("Move", Random.Range(1, 100));
            AddStat("Stamina", Random.Range(1, 100));
            AddStat("Dexterity", Random.Range(1, 100));
            AddStat("Intelligence", Random.Range(1, 100));
            AddStat("Damage", Random.Range(1, 100));
            AddStat("Regeneration", Random.Range(1, 100));
        }

        [Button]
        public void RemoveAllStats()
        {
            var stats = _userInfo.CharacterInfo.GetAllStats().ToList();
            foreach (var VARIABLE in stats)
            {
                RemoveStat(VARIABLE.Name);
            }
        }
    }
}