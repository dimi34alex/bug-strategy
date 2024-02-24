using System;
using Unit.Ants.Configs.Professions;
using Unit.ProfessionsCore;

namespace Unit.Ants.Professions
{
    [Serializable]
    public class AntMeleeWarriorProfession : MeleeWarriorProfession
    {
        public AntMeleeWarriorProfession(AntBase ant, AntMeleeWarriorConfig antHandItem)
            : base(ant, antHandItem.InteractionRange, antHandItem.Cooldown, antHandItem.AttackRange, antHandItem.Damage)
        { }
    }
}