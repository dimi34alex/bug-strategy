using BugStrategy.Constructions;
using BugStrategy.CustomTimer;
using BugStrategy.Unit.AbilitiesCore;
using UnityEngine;

namespace BugStrategy.Unit.Repairing
{
    public class RepairAbility : IActiveAbility
    {
        public AbilityType AbilityType => AbilityType.Repairing;
        public IReadOnlyTimer Cooldown { get; } = new Timer(1, 1);

        private readonly UnitBase _abilityOwner;
        private readonly RepairApplier _repairApplier;

        public RepairAbility(UnitBase abilityOwner, RepairApplier repairApplier)
        {
            _abilityOwner = abilityOwner;
            _repairApplier = repairApplier;
        }

        public void TryActivate()
        {
            _repairApplier.PrepareRepairing(this);
        }

        public void Apply(ConstructionBase constructionBase)
        {
            Debug.Log("Apply");
            _abilityOwner.HandleGiveOrder(constructionBase, UnitPathType.Repair_Construction);
        }

        public void Reset()
        {
            _repairApplier.TryCancelRepairing(this);
        }
    }
}