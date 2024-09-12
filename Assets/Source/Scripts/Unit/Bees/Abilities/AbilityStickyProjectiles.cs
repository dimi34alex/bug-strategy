using Source.Scripts.Unit.AbilitiesCore;

namespace Unit.Bees
{
    public sealed class AbilityStickyProjectiles : IPassiveAbility
    {
        private readonly HoneyCatapultAttackProcessor _honeyCatapultAttackProcessor;
        private readonly int _projectilesCounter;

        public AbilityType AbilityType => AbilityType.StickyProjectiles;

        public AbilityStickyProjectiles(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, int projectilesCount)
        {
            _honeyCatapultAttackProcessor = honeyCatapultAttackProcessor;
            _projectilesCounter = projectilesCount;
            
            _honeyCatapultAttackProcessor.SetProjectileCounterCapacity(_projectilesCounter);
        }
    }
}