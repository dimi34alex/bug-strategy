using BugStrategy.Projectiles.Factory;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit.Ants
{
    public sealed class AntRangeWarriorOrderValidator : AntWarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
        
        public override OrderValidatorBase OrderValidatorBase { get; }
        public override CooldownProcessor CooldownProcessor { get; }
        public override AttackProcessorBase AttackProcessor { get; }
        
        public AntRangeWarriorOrderValidator(AntBase ant, int professionRang, AntRangeWarriorConfig antHandItem, ProjectileFactory projectileFactory)
            : base(professionRang)
        {
            CooldownProcessor = new CooldownProcessor(antHandItem.Cooldown);
            AttackProcessor = new RangeAttackProcessor(ant, antHandItem.AttackRange, antHandItem.Damage, 
                CooldownProcessor, antHandItem.ProjectileType, projectileFactory);
            OrderValidatorBase = new WarriorOrderValidator(ant, antHandItem.InteractionRange, CooldownProcessor,
                AttackProcessor);

            AttackProcessor.OnEnterEnemyInZone += EnterInZone;
            OrderValidatorBase.OnEnterInZone += EnterInZone;
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            CooldownProcessor.HandleUpdate(time);
        }
    }
}
