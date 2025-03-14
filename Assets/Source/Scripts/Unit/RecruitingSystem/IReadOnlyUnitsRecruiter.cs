using System;
using System.Collections.Generic;
using BugStrategy.ResourcesSystem;

namespace BugStrategy.Unit.RecruitingSystem
{
    public interface IReadOnlyUnitsRecruiter
    {
        public IReadOnlyDictionary<UnitType, UnitRecruitingData> UnitRecruitingData { get; }
        public IReadOnlyList<IReadOnlyUnitRecruitingStack> Stacks { get; }
        
        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public IReadOnlyDictionary<ResourceID, int> GetUnitRecruitingCost(UnitType unitType);
        
        public bool HaveFreeStack();
        
        public int FindFreeStack();
    }
}