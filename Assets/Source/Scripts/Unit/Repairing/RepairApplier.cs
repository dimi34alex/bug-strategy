using BugStrategy.Selection;

namespace BugStrategy.Unit.Repairing
{
    public class RepairApplier
    {
        private readonly Selector _selector;

        private RepairAbility _activeRepairAbility;

        public RepairApplier(Selector selector)
        {
            _selector = selector;

            _selector.OnTrySelect += TryApplyRepair;
        }

        public void PrepareRepairing(RepairAbility repairAbility)
        {
            _activeRepairAbility = repairAbility;
        }

        public void TryCancelRepairing(RepairAbility repairAbility)
        {
            if (_activeRepairAbility == repairAbility)
                _activeRepairAbility = null;
        }

        private void TryApplyRepair()
        {
            if (_activeRepairAbility == null)
                return;
            
            if (_selector.ConstructionSelector.SelectedConstruction == null)
                return;
            
            _activeRepairAbility.Apply(_selector.ConstructionSelector.SelectedConstruction);
            _activeRepairAbility = null;
        }
    }
}