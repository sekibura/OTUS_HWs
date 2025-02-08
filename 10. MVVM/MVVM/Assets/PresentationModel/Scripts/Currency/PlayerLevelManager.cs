using System;
using Sirenix.OdinInspector;

namespace Lessons.Architecture.PM
{
    public sealed class PlayerLevelManager
    {
        public event Action OnLevelUp;
        public event Action<int> OnExperienceChanged;

        [ShowInInspector, ReadOnly]
        public int CurrentLevel { get; private set; } = 1;

        [ShowInInspector, ReadOnly]
        public int CurrentExperience { get; private set; }

        [ShowInInspector, ReadOnly]
        public int RequiredExperience
        {
            get { return 100 * (this.CurrentLevel + 1); }
        }

        public void SetData(int currentLevel, int currentExperience)
        {
            CurrentExperience = currentExperience;
            CurrentLevel = currentLevel;
        }
        
        public void AddExperience(int range)
        {
            var xp = Math.Min(this.CurrentExperience + range, this.RequiredExperience);
            this.CurrentExperience = xp;
            this.OnExperienceChanged?.Invoke(xp);
        }
        
        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                this.CurrentExperience = 0;
                this.CurrentLevel++;
                this.OnLevelUp?.Invoke();
            }
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience == this.RequiredExperience;
        }
    }
}