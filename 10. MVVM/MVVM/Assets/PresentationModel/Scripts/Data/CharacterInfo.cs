using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace OTUSHW.MVVM.UI.Data
{
    [Serializable]
    public sealed class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;
        public event Action<CharacterStat> OnStatValueChanged;

        [ShowInInspector, ReadOnly] private readonly HashSet<CharacterStat> stats = new();

        public void AddStat(CharacterStat stat)
        {
            if (stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

        public void RemoveStat(CharacterStat stat)
        {
            if (stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public void RemoveStat(string name)
        {
            CharacterStat stat = GetStat(name);
            
            if(stat == null)
               return;
            
            if (stats.Remove(stat))
            {
                OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public void SetStatValue(string name, int value)
        {
            CharacterStat stat = GetStat(name);
            stat.ChangeValue(value);
            OnStatValueChanged?.Invoke(stat);
        }

        public HashSet<CharacterStat> GetAllStats()
        {
            return stats;
        }

        public CharacterStat[] GetStats()
        {
            return stats.ToArray();
        }
    }
}