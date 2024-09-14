using BugStrategy.Unit;

namespace BugStrategy.Effects
{
    public sealed class PoisonProcessor : EffectProcessorBase, IDamageApplicator
    {
        private readonly IPoisonEffectable _poisonable;
        public float Damage { get; }
        
        public override EffectType EffectType => EffectType.Poison;

        public PoisonProcessor(IPoisonEffectable poisonable, float damagePerSecond, float existTime)
            : base(existTime)
        {
            _poisonable = poisonable;
            Damage = damagePerSecond;

            UpdateEvent += ApplyDamage;
        }

        private void ApplyDamage(float time)
        {
            _poisonable.TakeDamage(this, time);
        }
    }
}