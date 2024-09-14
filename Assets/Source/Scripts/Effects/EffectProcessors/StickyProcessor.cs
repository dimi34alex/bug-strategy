namespace BugStrategy.Effects
{
    public sealed class StickyProcessor : EffectProcessorBase
    {
        private readonly IStickyHoneyEffectable _stickyHoneyEffectable;
        
        public override EffectType EffectType => EffectType.StickyHoney;

        public StickyProcessor(IStickyHoneyEffectable stickyHoneyEffectable, float existTime) : base(existTime)
        {
            _stickyHoneyEffectable = stickyHoneyEffectable;
            ApplyEffect();
            ExistTimer.OnTimerEnd += RemoveEffect;
        }

        private void ApplyEffect()
            => _stickyHoneyEffectable.SwitchSticky(true);

        private void RemoveEffect()
            => _stickyHoneyEffectable.SwitchSticky(false);
    }
}