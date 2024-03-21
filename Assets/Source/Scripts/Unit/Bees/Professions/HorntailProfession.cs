using Projectiles.Factory;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;

namespace Unit.Bees
{
    public sealed class HorntailProfession : WarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
        
        public override AttackProcessorBase AttackProcessor { get; }
        
        public HorntailProfession(UnitBase unit, float interactionRange, float cooldown, float attackRange, 
            float damage, float damageRadius, ProjectileFactory projectilesFactory)
            : base(unit, interactionRange, cooldown)
        {
            AttackProcessor = new HorntailAttackProcessor(unit, attackRange, damage, damageRadius, CooldownProcessor, projectilesFactory);
            AttackProcessor.OnEnterEnemyInZone += EnterInZone;
        }
    }
}