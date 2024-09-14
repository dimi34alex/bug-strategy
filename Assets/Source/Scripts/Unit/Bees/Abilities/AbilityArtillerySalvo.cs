using BugStrategy.CustomTimer;
using BugStrategy.Unit.AbilitiesCore;

namespace BugStrategy.Unit.Bees
{
    public sealed class AbilityArtillerySalvo : IPassiveAbility
    {
        private readonly HoneyCatapultAttackProcessor _attackProcessor;
        private readonly float _constructionDamageScale;
        
        public IReadOnlyTimer Cooldown { get; } = new Timer(1, 1);
        public AbilityType AbilityType => AbilityType.ArtillerySalvo;

        public AbilityArtillerySalvo(HoneyCatapultAttackProcessor honeyCatapultAttackProcessor, float constructionDamageScale)
        {
            _attackProcessor = honeyCatapultAttackProcessor;
            _constructionDamageScale = constructionDamageScale;
            
            _attackProcessor.SetConstructionDamageScale(_constructionDamageScale);
        }
    }
}