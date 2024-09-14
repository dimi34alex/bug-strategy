using CycleFramework.Extensions;

namespace BugStrategy.Effects
{
    public class PoisonCreator : EffectCreatorBase
    {
        private readonly PoisonConfig _config;
        
        public override EffectType EffectType => EffectType.Poison;

        public PoisonCreator(PoisonConfig poisonConfig)
        {
            _config = poisonConfig;
        }
        
        public override bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor)
        {
            effectProcessor = null;
            if (effectable.TryCast(out IPoisonEffectable poisonable))
            {
                effectProcessor = new PoisonProcessor(poisonable, _config.DamagePerSecond, _config.DamagePerSecond);
                return true;
            }
            
            return false;
        }
    }
}