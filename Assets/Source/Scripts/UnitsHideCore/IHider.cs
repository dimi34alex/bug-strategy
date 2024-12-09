using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UnitsHideCore
{
    public interface IHider
    {
        public IReadOnlyList<UnitType> Access { get; }
        public IReadOnlyList<HiderCellBase> HiderCells { get; }
        
        public bool CheckAccess(UnitType unitType);

        public bool HaveFreePlace();
        
        public bool TryHideUnit(IHidableUnit unit);

        public UnitBase ExtractUnit(int index);

        public UnitBase ExtractUnit(int index, Vector3 extractPosition);

        public UnitBase ExtractUpgradeUnit (int index, Vector3 extractPosition);
    }
}