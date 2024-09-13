using Source.Scripts;
using Source.Scripts.ResourcesSystem;

namespace UnitsHideCore
{
    public abstract class HiderCellBase
    {
        public readonly UnitType UnitType;
        public readonly FloatStorage HealthPoints;

        protected HiderCellBase(UnitBase unit)
        {
            UnitType = unit.UnitType;
            HealthPoints = new FloatStorage(unit.HealthStorage.CurrentValue, unit.HealthStorage.Capacity);
        }

        public virtual void HandleUpdate(float time)
        {
            
        }
        
        public void Heal(float healValue)
        {
            HealthPoints.ChangeValue(healValue);
        }
    }
}