using BugStrategy.Selection;

namespace BugStrategy.Unit.Bees.WorkerBeeAttack
{
    public class HiveProtectionApplier
    {
        private readonly Selector _selector;

        private HiveProtectionAbility _activeHiveProtectionAbility;

        public HiveProtectionApplier(Selector selector)
        {
            _selector = selector;

            _selector.OnTrySelect += TryApplyAttack;
        }

        public void PrepareAttack(HiveProtectionAbility hiveProtectionAbility)
        {
            _activeHiveProtectionAbility = hiveProtectionAbility;
        }

        public void TryCancelAttack(HiveProtectionAbility hiveProtectionAbility)
        {
            if (_activeHiveProtectionAbility == hiveProtectionAbility)
                _activeHiveProtectionAbility = null;
        }

        private void TryApplyAttack()
        {
            if (_activeHiveProtectionAbility == null)
                return;
            
            if (_selector.UnitsSelector.GetEnemySelectedUnits().Count <= 0)
                return;
            
            _activeHiveProtectionAbility.Apply(_selector.UnitsSelector.GetEnemySelectedUnits()[0]);
            _activeHiveProtectionAbility = null;
        }
    }
}