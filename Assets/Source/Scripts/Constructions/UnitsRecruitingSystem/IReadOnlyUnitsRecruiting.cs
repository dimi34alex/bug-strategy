using System;
using System.Collections.Generic;

namespace UnitsRecruitingSystem
{
    public interface IReadOnlyUnitsRecruiting<TEnum>
        where TEnum : Enum
    {
        public event Action OnChange;
        public event Action OnRecruitUnit;
        public event Action OnAddStack;
        public event Action OnTick;
        public event Action OnCancelRecruit;

        public List<IReadOnlyUnitRecruitingStack<TEnum>> GetRecruitingInformation();
    }
}