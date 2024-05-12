using Unit.Effects.InnerProcessors;

namespace Unit.Effects.Interfaces
{
    public interface IMoveSpeedChangeEffectable
    {
        public MoveSpeedChangerProcessor MoveSpeedChangerProcessor { get; }
    }
}