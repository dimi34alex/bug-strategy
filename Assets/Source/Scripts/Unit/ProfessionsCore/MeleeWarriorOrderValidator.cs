using System;
using Unit.ProfessionsCore.Processors;

namespace Unit.ProfessionsCore
{
    [Serializable]
    public class MeleeWarriorOrderValidator : WarriorOrderValidatorBase
    {
        public MeleeWarriorOrderValidator(UnitBase unit, float interactionRange, CooldownProcessor cooldownProcessor, 
            AttackProcessorBase attackProcessor)
            : base(unit, interactionRange, cooldownProcessor, attackProcessor)
        {
            
        }
    }
}