using System.Linq;
using System.Text;
using OTUSHW.MVVM.UI.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace OTUSHW.MVVM.UI.Helpers
{
    public sealed class CharacterStatHelper : MonoBehaviour
    {
        [SerializeField]
        private UserInfoSO _userInfoSo;

        [Button]
        public void AddStat(string name, int value)
        {
            _userInfoSo.CharacterInfo.AddStat(new CharacterStat(name, value));
        }

        [Button]
        public void RemoveStat(string name)
        {
            _userInfoSo.CharacterInfo.RemoveStat(name);
        }

        [Button]
        private void PrintAllStats()
        {
            var stats = _userInfoSo.CharacterInfo.GetAllStats();
            StringBuilder sb = new StringBuilder();
            foreach (var stat in stats)
            {
                sb.Append($"{stat.ToString()} ");
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
            var stats = _userInfoSo.CharacterInfo.GetAllStats().ToList();
            foreach (var stat in stats)
            {
                RemoveStat(stat.Name);
            }
        }
    }
}