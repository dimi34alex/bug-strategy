using CycleFramework.Extensions;

namespace BugStrategy.Effects
{
    public class MoveSpeedDownCreator : EffectCreatorBase
    {
        private readonly MoveSpeedDownConfig _config;
        
        public override EffectType EffectType => EffectType.MoveSpeedUp;

        public MoveSpeedDownCreator(MoveSpeedDownConfig config)
        {
            _config = config;
        }
        
        public override bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor)
        {
            effectProcessor = null;
            if (effectable.TryCast(out IMoveSpeedChangeEffectable moveSpeedUpEffectable))
            {
                effectProcessor =
                    new MoveSpeedDownProcessor(moveSpeedUpEffectable, _config.PowerInPercentage, _config.ExistTime);
                return true;
            }

            return false;
        }
    }
}