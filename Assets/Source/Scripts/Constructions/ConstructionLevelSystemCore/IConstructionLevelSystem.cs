using System;

namespace Constructions.LevelSystemCore
{
    public interface IConstructionLevelSystem
    {
        public int CurrentLevelIndex { get; }
        
        public event Action OnLevelUp;

        public void Init(int initialLevelIndex);
        
        public bool LevelCapCheck();
        
        /// <summary>
        /// level up with checks. For level up without checks use LevelUp()
        /// </summary>
        public bool TryLevelUp();
        
        /// <summary>
        /// level up without any checks. For checks use TryLevelUp()
        /// </summary>
        public void LevelUp();
        
        public bool LevelUpPriceCheck();
    }
}