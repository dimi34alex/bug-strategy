using Unit.Effects.Configs;
using Unit.Effects.EffectProcessors;
using Unit.Effects.Interfaces;

namespace Unit.Effects.EffectsCreators
{
    public class StickyHoneyCreator : EffectCreatorBase
    {
        private readonly StickyHoneyConfig _config;
        
        public override EffectType EffectType => EffectType.StickyHoney;

        public StickyHoneyCreator(StickyHoneyConfig config)
        {
            _config = config;
        }
        
        public override bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor)
        {
            effectProcessor = null;
            if (effectable.TryCast(out IStickyHoneyEffectable stickyHoneyEffectable))
            {
                effectProcessor = new StickyProcessor(stickyHoneyEffectable, _config.ExistTime);
                return true;
            }

            return false;
        }
    }
}