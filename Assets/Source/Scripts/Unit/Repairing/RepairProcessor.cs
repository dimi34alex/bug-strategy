using BugStrategy.Constructions;
using BugStrategy.CustomTimer;

namespace BugStrategy.Unit.Repairing
{
    public class RepairProcessor : IRepairApplicator
    {
        public float Repair { get; }
        public ConstructionBase Construction { get; private set; }

        private readonly Timer _repairCooldown;

        public RepairProcessor(float repairValue, float repairCooldown)
        {
            Repair = repairValue;
            _repairCooldown = new Timer(repairCooldown, 0, true);
        }
        
        public void SetConstruction(ConstructionBase construction)
        {
            Construction = construction;
            _repairCooldown.Reset();
            
            if (Construction == null) 
                _repairCooldown.SetPause();
            else
                _repairCooldown.Continue();
        }

        public void HandleUpdate(float deltaTime)
        {
            _repairCooldown.Tick(deltaTime);

            if (_repairCooldown.TimerIsEnd)
            {
                Construction.TakeRepair(this);
                _repairCooldown.Reset();
            }
        }

        public void Reset()
        {
            SetConstruction(null);
        }
    }
}