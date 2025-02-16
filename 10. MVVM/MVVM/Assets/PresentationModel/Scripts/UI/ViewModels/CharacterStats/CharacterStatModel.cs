using System;
using OTUSHW.MVVM.UI.Data;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public sealed class CharacterStatModel : ICharacterStatModel
    {
        public string Name { get; private set; }
        public int Value { get; private set; }

        public event Action<int> OnStatValueChanged;
        public event Action<string> OnStatNameChanged;

        private CharacterInfo _characterInfo;

        public CharacterStatModel(string name, int value, CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;
            Name = name;
            Value = value;
            if(_characterInfo != null)
                Subscribe();
        }

        private void Subscribe()
        {
            _characterInfo.OnStatValueChanged += StatValueChanged;
        }

        private void Unsubscribe()
        {
            _characterInfo.OnStatValueChanged -= StatValueChanged;
        }

        private void StatValueChanged(CharacterStat stat)
        {
            if (stat.Name == Name)
            {
                Value = stat.Value;
                OnStatValueChanged?.Invoke(stat.Value);
            }
        }
        
        public void Dispose()
        {
            if(_characterInfo != null)
                Unsubscribe();
        }
    }
}