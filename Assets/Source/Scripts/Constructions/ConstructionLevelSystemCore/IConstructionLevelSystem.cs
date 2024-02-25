using System;

namespace Constructions.LevelSystemCore
{
    public interface IConstructionLevelSystem
    {
        public int CurrentLevelNum { get; }
        public event Action OnLevelUp;
        public bool LevelCapCheck();
        public bool PriceCheck();
        public bool TryLevelUp();
    }
}