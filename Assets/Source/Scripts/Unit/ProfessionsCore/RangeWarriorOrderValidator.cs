using Unit.ProfessionsCore.Processors;

namespace Unit.ProfessionsCore
{
    public class RangeWarriorOrderValidator : WarriorOrderValidatorBase
    {
        public RangeWarriorOrderValidator(UnitBase unit, float interactionRange, CooldownProcessor cooldownProcessor,
            AttackProcessorBase attackProcessor)
            : base(unit, interactionRange, cooldownProcessor, attackProcessor)
        {
        }
    }
}