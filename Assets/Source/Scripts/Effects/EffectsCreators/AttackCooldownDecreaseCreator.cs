using CycleFramework.Extensions;

namespace BugStrategy.Effects
{
    public class AttackCooldownDecreaseCreator : EffectCreatorBase
    {
        private readonly AttackCooldownDecreaseConfig _config;
        
        public override EffectType EffectType => EffectType.AttackCooldownDecrease;

        public AttackCooldownDecreaseCreator(AttackCooldownDecreaseConfig config)
        {
            _config = config;
        }
        
        public override bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor)
        {
            effectProcessor = null;
            if (effectable.TryCast(out IAttackCooldownChangerEffectable attackCooldownChangerEffect))
            {
                effectProcessor = new AttackCooldownDecreaseProcessor(attackCooldownChangerEffect,
                    _config.PowerInPercentage, _config.ExistTime);
                return true;
            }

            return false;
        }
    }
}