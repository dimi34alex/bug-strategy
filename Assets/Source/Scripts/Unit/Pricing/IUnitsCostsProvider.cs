using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using BugStrategy.Unit.RecruitingSystem;

namespace BugStrategy.Unit.Pricing
{
    public interface IUnitsCostsProvider
    {
        public IReadOnlyDictionary<ResourceID, int> GetUnitRecruitingCost(UnitType unitType);
    }
}