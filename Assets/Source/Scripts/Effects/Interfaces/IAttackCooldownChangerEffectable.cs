using Unit.Effects.InnerProcessors;

namespace Unit.Effects.Interfaces
{
    public interface IAttackCooldownChangerEffectable
    {
        public AttackCooldownChanger AttackCooldownChanger { get; }
    }
}