using System;
using System.Collections.Generic;
using OTUSHW.MVVM.UI.Data;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public interface ICharacterStatsModel : IDisposable
    {
        public List<CharacterStatModel> CharacterStats { get;}

        public event Action<CharacterStat> OnCharacterStatChanged;
        public event Action<CharacterStat> OnCharacterStatAdded;
        public event Action<CharacterStat> OnCharacterStatRemoved;
    }
}