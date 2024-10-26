using System;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;

namespace BugStrategy.Unit.Ants
{
    [Serializable]
    public sealed class AntMeleeWarriorProfession : AntWarriorProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;

        public override OrderValidatorBase OrderValidatorBase { get; }
        public override CooldownProcessor CooldownProcessor { get; }
        public override AttackProcessorBase AttackProcessor { get; }
        
        public AntMeleeWarriorProfession(AntBase ant, int professionRang, AntMeleeWarriorConfig antHandItem)
            : base(professionRang)
        {
            CooldownProcessor = new CooldownProcessor(antHandItem.Cooldown);
            AttackProcessor = new MeleeAttackProcessor(ant, antHandItem.AttackRange, antHandItem.Damage, CooldownProcessor);
            OrderValidatorBase = new WarriorOrderValidator(ant, antHandItem.AttackRange, CooldownProcessor, AttackProcessor);
           
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