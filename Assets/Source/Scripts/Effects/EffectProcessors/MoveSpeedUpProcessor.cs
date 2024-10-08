namespace BugStrategy.Effects
{
    public sealed class MoveSpeedUpProcessor : EffectProcessorBase
    {
        private readonly IMoveSpeedChangeEffectable _moveSpeedChangeEffectable;
        private readonly float _power;

        public override EffectType EffectType => EffectType.MoveSpeedUp;

        public MoveSpeedUpProcessor(IMoveSpeedChangeEffectable moveSpeedChangeEffectable, float power, float existTime) 
            : base(existTime)
        {
            _moveSpeedChangeEffectable = moveSpeedChangeEffectable;
            _power = power;
            ExistTimer.OnTimerEnd += RemoveEffect;
            ApplyEffect();
        }

        private void ApplyEffect()
        {
            _moveSpeedChangeEffectable.MoveSpeedChangerProcessor.ApplyEffect(_power);
        }

        private void RemoveEffect()
        {
            _moveSpeedChangeEffectable.MoveSpeedChangerProcessor.RemoveEffect(_power);
        }
    }
}