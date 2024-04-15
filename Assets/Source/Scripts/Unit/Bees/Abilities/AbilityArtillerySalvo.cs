using Unit.AbilitiesCore;

namespace Unit.Bees
{
    public sealed class AbilityArtillerySalvo : IAbility
    {
        private readonly HoneyCatapultAttackProcessor _attackProcessor;
        private readonly float _constructionDamageScale;
        
        public AbilityType AbilityType => AbilityType.ArtillerySalvo;

        public AbilityArtillerySalvo(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, float constructionDamageScale)
        {
            _attackProcessor = honeyCatapultAttackProcessor;
            _constructionDamageScale = constructionDamageScale;
            
            _attackProcessor.SetConstructionDamageScale(_constructionDamageScale);
        }
    }
}