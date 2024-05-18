using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using UnitsHideCore;

namespace Unit.Bees
{
    public class HidableWarriorOrderValidator : OrderValidatorBase
    {
        private readonly AttackProcessorBase _attackProcessor;
        
        public HidableWarriorOrderValidator(UnitBase unit, float interactionRange, CooldownProcessor cooldownProcessor, AttackProcessorBase attackProcessor)
            : base(unit, interactionRange)
        {
            _attackProcessor = attackProcessor;
            _attackProcessor.OnEnterEnemyInZone += EnterInZone;
        }
        
        public override bool CheckDistance(UnitPathData pathData)
        {
            switch (pathData.PathType)
            {
                case UnitPathType.Attack:
                    return pathData.Target.IsAnyNull()
                        ? _attackProcessor.CheckEnemiesInAttackZone()
                        : _attackProcessor.CheckAttackDistance(pathData.Target);
                default:
                    return !pathData.Target.IsAnyNull() && CheckInteraction(pathData.Target);
            }
        }
        
        protected override UnitPathData ValidateAutoOrder(IUnitTarget target)
        {
            if (target.IsAnyNull())
                return new UnitPathData(null, UnitPathType.Move);

            switch (target.TargetType)
            {
                case (UnitTargetType.Other_Unit):
                    if (target.Affiliation != Affiliation && target.CastPossible<IDamagable>())
                        return new UnitPathData(target, UnitPathType.Attack);
                    break;
                case (UnitTargetType.Construction):
                    if (target.Affiliation != Affiliation && target.CastPossible<IDamagable>())
                        return new UnitPathData(target, UnitPathType.Attack);
                    if (target.Affiliation == Affiliation && target.TryCast(out IHiderConstruction hiderConstruction) 
                                                          && hiderConstruction.Hider.CheckAccess(Unit.UnitType))
                        return new UnitPathData(target, UnitPathType.HideInConstruction);
                    break;
            }

            return new UnitPathData(null, UnitPathType.Move);
        }
        
        protected override UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType)
        {
            switch (pathType)
            {
                case UnitPathType.Attack:
                    if (!target.IsAnyNull() && 
                        target.Affiliation != Affiliation && 
                        target.CastPossible<IDamagable>() 
                        || _attackProcessor.CheckEnemiesInAttackZone())
                        return UnitPathType.Attack;
                    break;
                case UnitPathType.Switch_Profession:
                    if (!target.IsAnyNull() && 
                        Unit.Fraction == FractionType.Ants && 
                        target.TargetType == UnitTargetType.Construction)
                        return UnitPathType.Switch_Profession;
                    break;
                case UnitPathType.HideInConstruction:
                    if (!target.IsAnyNull() && target.Affiliation == Affiliation &&
                        target.TryCast(out IHiderConstruction hiderConstruction) &&
                        hiderConstruction.Hider.CheckAccess(Unit.UnitType))
                        return UnitPathType.HideInConstruction;
                    break;
            }

            return UnitPathType.Move;
        }
    }
}