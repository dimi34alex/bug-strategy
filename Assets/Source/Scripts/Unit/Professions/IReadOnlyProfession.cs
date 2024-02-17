using System;

namespace Unit.Professions
{
    public interface IReadOnlyProfession
    {
        public ProfessionType ProfessionType { get; } 
        public event Action OnEnterInZone;

        public bool CheckDistance(UnitPathData pathData);
        public UnitPathData AutoGiveOrder(IUnitTarget unitTarget);
        public UnitPathData HandleGiveOrder(IUnitTarget unitTarget, UnitPathType pathType);
    }
}