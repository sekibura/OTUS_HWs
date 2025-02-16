using System;
using System.Collections.Generic;
using OTUSHW.MVVM.UI.Data;

namespace OTUSHW.MVVM.UI.ViewModel
{
    public sealed class CharacterStatsModel : ICharacterStatsModel
    {
        public List<CharacterStatModel> CharacterStats { get; private set; } = new();
        public event Action<CharacterStat> OnCharacterStatChanged;
        public event Action<CharacterStat> OnCharacterStatAdded;
        public event Action<CharacterStat> OnCharacterStatRemoved;

        private CharacterInfo _characterInfo;
        public CharacterStatsModel(CharacterInfo characterInfo)
        {
            _characterInfo = characterInfo;

            if (_characterInfo != null)
            {
                InitStats();
                Subscribe();
            }
        }

        private void InitStats()
        {
            var stats = _characterInfo.GetStats();
            for (var index = 0; index < stats.Length; index++)
            {
                var stat = stats[index];
                CharacterStats.Add(new CharacterStatModel(stat.Name, stat.Value, _characterInfo));
            }
        }

        private void Subscribe()
        {
            _characterInfo.OnStatValueChanged += StatValueChanged;
            _characterInfo.OnStatAdded += StatAdded;
            _characterInfo.OnStatRemoved += StatRemoved;
        }

        private void Unsubscribe()
        {
            _characterInfo.OnStatValueChanged -= StatValueChanged;
            _characterInfo.OnStatAdded -= StatAdded;
            _characterInfo.OnStatRemoved -= StatRemoved;
        }

        private void StatValueChanged(CharacterStat stat)
        {
            OnCharacterStatChanged?.Invoke(stat);
        }

        private void StatRemoved(CharacterStat stat)
        {
            var statForRemove = CharacterStats.Find(x => x.Name == stat.Name);

            if ( stat != null)
            {
                CharacterStats.Remove(statForRemove);
            }

            OnCharacterStatRemoved?.Invoke(stat);
        }

        private void StatAdded(CharacterStat stat)
        {
            var statForAdd = CharacterStats.Find(x => x.Name == stat.Name);

            if (statForAdd == null)
            {
                statForAdd = new CharacterStatModel(stat.Name, stat.Value, _characterInfo);
                CharacterStats.Add(statForAdd);
            }

            OnCharacterStatAdded?.Invoke(stat);
        }

        public void Dispose()
        {
            Unsubscribe();

            for (var index = 0; index < CharacterStats.Count; index++)
            {
                var statModel = CharacterStats[index];
                statModel.Dispose();
            }
        }
    }
}