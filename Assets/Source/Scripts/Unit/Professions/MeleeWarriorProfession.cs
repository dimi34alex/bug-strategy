using System;
using Unit.Professions.Processors;

namespace Unit.Professions
{
    [Serializable]
    public class MeleeWarriorProfession : WarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;

        public override AttackProcessorBase AttackProcessor { get; }
        
        public MeleeWarriorProfession(UnitBase unit, float interactionRange, float attackCooldown, float attackRange,
            float damage)
            : base(unit, interactionRange, attackCooldown)
        {
            AttackProcessor = new MeleeAttackProcessor(unit, attackRange, damage, CooldownProcessor);
            AttackProcessor.OnEnterEnemyInZone += EnterInZone;
        }
    }
}