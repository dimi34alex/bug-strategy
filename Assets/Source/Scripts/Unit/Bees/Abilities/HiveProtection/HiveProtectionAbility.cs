using BugStrategy.CustomTimer;
using BugStrategy.Unit.AbilitiesCore;
using UnityEngine;

namespace BugStrategy.Unit.Bees.WorkerBeeAttack
{
    public class HiveProtectionAbility : IActiveAbility
    {
        public AbilityType AbilityType => AbilityType.HiveProtection;
        public IReadOnlyTimer Cooldown { get; } = new Timer(1, 1);

        private readonly UnitBase _abilityOwner;
        private readonly HiveProtectionApplier _hiveProtectionApplier;

        public HiveProtectionAbility(UnitBase abilityOwner, HiveProtectionApplier hiveProtectionApplier)
        {
            _abilityOwner = abilityOwner;
            _hiveProtectionApplier = hiveProtectionApplier;
        }

        public void TryActivate()
        {
            _hiveProtectionApplier.PrepareAttack(this);
        }

        public void Apply(ITarget target)
        {
            Debug.Log("Apply");
            _abilityOwner.HandleGiveOrder(target, UnitPathType.Attack);
        }

        public void Reset()
        {
            _hiveProtectionApplier.TryCancelAttack(this);
        }
    }
}