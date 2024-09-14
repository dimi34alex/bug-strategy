namespace BugStrategy.Effects
{
    public class AttackCooldownDecreaseProcessor : EffectProcessorBase
    {
        private readonly IAttackCooldownChangerEffectable _attackCooldownChangerEffectable;
        private readonly float _power;
        
        public override EffectType EffectType => EffectType.AttackCooldownDecrease;

        public AttackCooldownDecreaseProcessor(IAttackCooldownChangerEffectable attackCooldownChangerEffectable, float power, float existTime) 
            : base(existTime)
        {
            _attackCooldownChangerEffectable = attackCooldownChangerEffectable;
            _power = power;
            ExistTimer.OnTimerEnd += RemoveEffect;
            ApplyEffect();
        }

        private void ApplyEffect()
        {
            _attackCooldownChangerEffectable.AttackCooldownChanger.ApplyEffect(_power);
        }

        private void RemoveEffect()
        {
            _attackCooldownChangerEffectable.AttackCooldownChanger.RemoveEffect(_power);
        }
    }
}