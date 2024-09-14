using CycleFramework.Extensions;

namespace BugStrategy.Effects
{
    public class MoveSpeedUpCreator : EffectCreatorBase
    {
        private readonly MoveSpeedUpConfig _config;
        
        public override EffectType EffectType => EffectType.MoveSpeedUp;

        public MoveSpeedUpCreator(MoveSpeedUpConfig config)
        {
            _config = config;
        }
        
        public override bool Create(IEffectable effectable, out EffectProcessorBase effectProcessor)
        {
            effectProcessor = null;
            if (effectable.TryCast(out IMoveSpeedChangeEffectable moveSpeedUpEffectable))
            {
                effectProcessor =
                    new MoveSpeedUpProcessor(moveSpeedUpEffectable, _config.PowerInPercentage, _config.ExistTime);
                return true;
            }

            return false;
        }
    }
}