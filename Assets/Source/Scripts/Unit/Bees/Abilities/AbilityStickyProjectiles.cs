namespace Unit.Bees
{
    public sealed class AbilityStickyProjectiles
    {
        private readonly HoneyCatapultAttackProcessor _honeyCatapultAttackProcessor;
        private readonly int _projectilesCounter;

        public AbilityStickyProjectiles(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, int projectilesCount)
        {
            _honeyCatapultAttackProcessor = honeyCatapultAttackProcessor;
            _projectilesCounter = projectilesCount;
            
            _honeyCatapultAttackProcessor.SetProjectileCounterCapacity(_projectilesCounter);
        }
    }
}