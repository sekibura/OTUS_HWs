using OTUSHW.MVVM.UI.ViewModel;
using TMPro;
using UnityEngine;

namespace OTUSHW.MVVM.UI.View
{
    public sealed class CharacterStatView : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text _statName;
        
        [SerializeField]
        private TMP_Text _statValue;
        
        private ICharacterStatModel _characterStatModel;

        public void Show(ICharacterStatModel characterStatModel)
        {
            _characterStatModel = characterStatModel;
            if (_characterStatModel != null)
            {
                Subscribe();
                UpdateStatValue(_characterStatModel.Value);
                UpdateStatName(_characterStatModel.Name);
            }
        }

        private void Subscribe()
        {
            _characterStatModel.OnStatValueChanged += UpdateStatValue;
            _characterStatModel.OnStatNameChanged += UpdateStatName;
        }

        private void Unsubscribe()
        {
            _characterStatModel.OnStatValueChanged -= UpdateStatValue;
            _characterStatModel.OnStatNameChanged -= UpdateStatName;
        }

        private void UpdateStatValue(int value)
        {
            _statValue.text = value.ToString();
        }

        private void UpdateStatName(string value)
        {
            _statName.text = value;
        }

        private void OnDisable()
        {
            if(_characterStatModel != null)
                Unsubscribe();
        }
    }
}