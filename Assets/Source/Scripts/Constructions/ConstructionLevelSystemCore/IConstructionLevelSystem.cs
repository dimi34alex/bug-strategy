using System;

namespace Constructions.LevelSystemCore
{
    public interface IConstructionLevelSystem
    {
        public int CurrentLevelIndex { get; }
        public event Action OnLevelUp;

        public bool LevelCapCheck();
        public bool LevelUpPriceCheck();
        public bool TryLevelUp();
    }
}