using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterStat
    {
        public event Action<int> OnValueChanged; 

        [ShowInInspector, ReadOnly]
        public string Name { get; private set; }

        [ShowInInspector, ReadOnly]
        public int Value { get; private set; }
        
        public void ChangeValue(int value)
        {
            this.Value = value;
            this.OnValueChanged?.Invoke(value);
        }

        public CharacterStat(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}