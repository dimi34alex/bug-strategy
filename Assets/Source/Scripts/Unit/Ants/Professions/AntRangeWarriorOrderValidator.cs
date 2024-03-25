using Projectiles.Factory;
using Unit.Ants.Configs.Professions;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;

namespace Unit.Ants.Professions
{
    public sealed class AntRangeWarriorOrderValidator : AntWarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
        
        public override OrderValidatorBase OrderValidatorBase { get; }
        public override CooldownProcessor CooldownProcessor { get; }
        public override AttackProcessorBase AttackProcessor { get; }
        
        public AntRangeWarriorOrderValidator(AntBase ant, AntRangeWarriorConfig antHandItem, ProjectileFactory projectileFactory)
            : base(antHandItem.AntProfessionRang)
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
