using System;
using System.Collections.Generic;

namespace UnitsRecruitingSystemCore
{
    public interface IReadOnlyUnitsRecruiter
    {
        public IReadOnlyList<UnitRecruitingData> UnitRecruitingData { get; }
        
        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public bool HaveFreeStack();
        
        public List<IReadOnlyUnitRecruitingStack> GetRecruitingInformation();
        public int FindFreeStack();
    }
}