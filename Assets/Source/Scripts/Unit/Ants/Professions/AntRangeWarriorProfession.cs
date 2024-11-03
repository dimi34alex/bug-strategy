using BugStrategy.Projectiles.Factory;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit.Ants
{
    public sealed class AntRangeWarriorProfession : AntWarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
        
        public override OrderValidatorBase OrderValidatorBase { get; }
        public override CooldownProcessor CooldownProcessor { get; }
        public override AttackProcessorBase AttackProcessor { get; }
        
        public AntRangeWarriorProfession(AntBase ant, int professionRang, AntRangeWarriorConfig antHandItem, ProjectilesFactory projectilesFactory)
            : base(professionRang)
        {
            CooldownProcessor = new CooldownProcessor(antHandItem.Cooldown);
            AttackProcessor = new RangeAttackProcessor(ant, antHandItem.AttackRange, antHandItem.Damage, 
                CooldownProcessor, antHandItem.ProjectileType, projectilesFactory);
            OrderValidatorBase = new WarriorOrderValidator(ant, antHandItem.InteractionRange, CooldownProcessor,
                AttackProcessor);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            CooldownProcessor.HandleUpdate(time);
        }
    }
}
