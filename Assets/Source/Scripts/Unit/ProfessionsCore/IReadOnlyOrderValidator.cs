using System;

namespace Unit.ProfessionsCore
{
    public interface IReadOnlyOrderValidator
    {
        public event Action OnEnterInZone;

        public bool CheckDistance(UnitPathData pathData);
        public UnitPathData AutoGiveOrder(IUnitTarget target);
        public UnitPathData HandleGiveOrder(IUnitTarget unitTarget, UnitPathType pathType);
    }
}