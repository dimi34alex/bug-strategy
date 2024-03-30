namespace Unit.Bees
{
    public class AbilityStickyProjectiles
    {
        private readonly HoneyCatapultAttackProcessor _honeyCatapultAttackProcessor;
        private readonly int _projectilesCounter;

        public AbilityStickyProjectiles(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, int projectilesCount)
        {
            _honeyCatapultAttackProcessor = honeyCatapultAttackProcessor;
            _projectilesCounter = projectilesCount;
            
            _honeyCatapultAttackProcessor.SetStickTileNum(_projectilesCounter);
        }
    }
}