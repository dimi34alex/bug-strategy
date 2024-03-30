namespace Unit.Bees
{
    public class AbilityArtillerySalvo
    {
        private readonly HoneyCatapultAttackProcessor _attackProcessor;
        private readonly float _constructionDamageScale;
        
        public AbilityArtillerySalvo(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, float constructionDamageScale)
        {
            _attackProcessor = honeyCatapultAttackProcessor;
            _constructionDamageScale = constructionDamageScale;
            
            _attackProcessor.SetConstructionDamageScale(_constructionDamageScale);
        }
    }
}