using System;
using Unit.ProfessionsCore.Processors;

namespace Unit.ProfessionsCore
{
    [Serializable]
    public abstract class WarriorProfessionBase : ProfessionBase
    {
        protected readonly CooldownProcessor CooldownProcessor;
        
        public abstract AttackProcessorBase AttackProcessor { get; }
        public IReadOnlyCooldownProcessor Cooldown => CooldownProcessor;
        public bool CanAttack => !CooldownProcessor.IsCooldown;
        
        protected WarriorProfessionBase(UnitBase unit, float interactionRange, float attackCooldown)
            : base(unit, interactionRange)
        {
            CooldownProcessor = new CooldownProcessor(attackCooldown);
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            CooldownProcessor.HandleUpdate(time);
        }
        
        public override bool CheckDistance(UnitPathData pathData)
        {
            switch (pathData.PathType)
            {
                case UnitPathType.Attack:
                    return pathData.Target.IsAnyNull()
                        ? AttackProcessor.CheckEnemiesInAttackZone()
                        : AttackProcessor.CheckAttackDistance(pathData.Target);
                default:
                    return !pathData.Target.IsAnyNull() && CheckInteraction(pathData.Target);
            }
        }
        
        protected override UnitPathData ValidateAutoOrder(IUnitTarget target)
        {
            if (target.IsAnyNull() ||
                !target.CastPossible<IDamagable>() ||
                target.Affiliation == Affiliation)
                return new UnitPathData(null, UnitPathType.Move);

            switch (target.TargetType)
            {
                case (UnitTargetType.Other_Unit):
                    return new UnitPathData(target, UnitPathType.Attack);
                case (UnitTargetType.Construction):
                    return new UnitPathData(target, UnitPathType.Attack);
                default:
                    return new UnitPathData(null, UnitPathType.Move);
            }
        }
        
        protected override UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType)
        {
            switch (pathType)
            {
                case UnitPathType.Attack:
                    if (!target.IsAnyNull() && target.Affiliation != Affiliation && target.CastPossible<IDamagable>() 
                        || AttackProcessor.CheckEnemiesInAttackZone())
                        return UnitPathType.Attack;
                    break;
                case UnitPathType.Switch_Profession:
                    if (!target.IsAnyNull() && Affiliation == AffiliationEnum.Ants &&
                        target.TargetType == UnitTargetType.Construction)
                        // TODO: create construction for switching professions
                        return UnitPathType.Switch_Profession;
                    break;
            }

            return UnitPathType.Move;
        }
    }
}