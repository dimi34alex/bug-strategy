using Unit.Ants.Configs.Professions;
using Unit.ProfessionsCore.Processors;

namespace Unit.Ants.Professions
{
    public abstract class AntWarriorProfessionBase : AntProfessionBase
    {
        public abstract CooldownProcessor CooldownProcessor { get; }
        public abstract AttackProcessorBase AttackProcessor { get; }
        
        public bool CanAttack => !CooldownProcessor.IsCooldown;
        
        protected AntWarriorProfessionBase(AntProfessionRang antProfessionRang) : base(antProfessionRang)
        {
        }
    }
}