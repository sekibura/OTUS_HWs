using TMPro;
using UnityEngine;

namespace Lessons.Architecture.PM.UI
{
    public sealed class CharacterStatItemPresenter : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _statName;
        [SerializeField] 
        private TMP_Text _statValue;

        public void Set(CharacterStat stat)
        {
            _statName.text = stat.Name + ": ";
            _statValue.text = stat.Value.ToString();
        }
        
    }
}
