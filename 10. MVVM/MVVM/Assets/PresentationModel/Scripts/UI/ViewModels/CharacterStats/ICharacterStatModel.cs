using System;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public interface ICharacterStatModel: IDisposable
    {
        public string Name { get; }
        public int Value { get; }

        public event Action<int> OnStatValueChanged;
        public event Action<string> OnStatNameChanged;
    }
}