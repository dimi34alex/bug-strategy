using System;
using Unit.Ants;
using Unit.Ants.ProfessionsConfigs;

namespace Unit.Professions.Ants
{
    [Serializable]
    public class AntMeleeWarriorProfession : MeleeWarriorProfession
    {
        public AntMeleeWarriorProfession(AntBase ant, AntMeleeWarriorConfig antHandItem)
            : base(ant, antHandItem.InteractionRange, antHandItem.Cooldown, antHandItem.AttackRange, antHandItem.Damage)
        { }
    }
}