using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit.Ants
{
    public abstract class AntWarriorProfessionBase : AntProfessionBase
    {
        public abstract CooldownProcessor CooldownProcessor { get; }
        public abstract AttackProcessorBase AttackProcessor { get; }
        
        public bool CanAttack => !CooldownProcessor.IsCooldown;
        
        protected AntWarriorProfessionBase(int antProfessionRang) : base(antProfessionRang)
        {
        }
    }
}