namespace UnitsHideCore
{
    public abstract class HiderCellBase
    {
        public readonly UnitType UnitType;
        public readonly ResourceStorage HealthPoints;

        protected HiderCellBase(UnitBase unit)
        {
            UnitType = unit.UnitType;
            HealthPoints = new ResourceStorage(unit.HealthStorage.CurrentValue, unit.HealthStorage.Capacity);
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