namespace Unit.Effects
{
    public abstract class EffectCreatorBase
    {
        public abstract EffectType EffectType { get; }
        
        public abstract bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor);
    }
}