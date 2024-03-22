using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;

namespace Unit.Bees
{
    public sealed class HorntailOrderValidator : WarriorOrderValidatorBase
    {
        public HorntailOrderValidator(UnitBase unit, float interactionRange, CooldownProcessor cooldownProcessor, 
            AttackProcessorBase attackProcessor)
            : base(unit, interactionRange, cooldownProcessor, attackProcessor)
        {
            
        }
    }
}