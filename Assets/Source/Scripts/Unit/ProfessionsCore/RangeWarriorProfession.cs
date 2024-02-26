using Projectiles;
using Projectiles.Factory;
using Unit.ProfessionsCore.Processors;

namespace Unit.ProfessionsCore
{
    public class RangeWarriorProfession : WarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior; 

        public override AttackProcessorBase AttackProcessor { get; }
        
        public RangeWarriorProfession(UnitBase unit, float interactionRange, float cooldown,
            float attackRange, float damage, ProjectileType projectileType, ProjectileFactory projectilesFactory)
            : base(unit, interactionRange, cooldown)
        {
            AttackProcessor = new RangeAttackProcessor(unit, attackRange, damage, CooldownProcessor, projectileType, projectilesFactory);
            AttackProcessor.OnEnterEnemyInZone += EnterInZone;
        }
    }
}