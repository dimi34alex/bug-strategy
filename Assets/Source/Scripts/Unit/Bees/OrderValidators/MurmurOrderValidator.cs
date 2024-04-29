using Construction.TownHalls;
using Unit.OrderValidatorCore;
using Unit.ProcessorsCore;
using UnitsHideCore;

namespace Unit.Bees
{
    public sealed class MurmurOrderValidator : OrderValidatorBase
    {
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        private readonly AttackProcessorBase _attackProcessor;
        
        public MurmurOrderValidator(UnitBase unit, float interactionRange, AttackProcessorBase attackProcessor, 
            ResourceExtractionProcessor resourceExtractionProcessor) 
            : base(unit, interactionRange)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
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
            if(target.IsAnyNull())
                return new UnitPathData(null, UnitPathType.Move);
            
            switch (target.TargetType)
            {
                case UnitTargetType.ResourceSource:
                    if (!_resourceExtractionProcessor.GotResource &&
                        target.Cast<ResourceSourceBase>().CanBeCollected)
                        return new UnitPathData(target, UnitPathType.Collect_Resource);
                    break;
                case UnitTargetType.Construction:
                    if(target.Affiliation != Affiliation)
                        return new UnitPathData(target, UnitPathType.Attack);

                    if (target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return new UnitPathData(target, UnitPathType.Storage_Resource);  
                    
                    if (target.Affiliation == Affiliation && target.TryCast(out IHiderConstruction hiderConstruction) 
                                                          && hiderConstruction.Hider.CheckAccess(Unit.UnitType))
                        return new UnitPathData(target, UnitPathType.HideInConstruction);
                    break;      
                case (UnitTargetType.Other_Unit):
                    return new UnitPathData(target, UnitPathType.Attack);
            }
            
            return new UnitPathData(null, UnitPathType.Move);
        }

        protected override UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType)
        {
            if (target.IsAnyNull()) 
                return UnitPathType.Move;
            
            switch (pathType)
            {
                case UnitPathType.Collect_Resource:
                    if (target.TargetType == UnitTargetType.ResourceSource &&
                        target.Cast<ResourceSourceBase>().CanBeCollected &&
                        !_resourceExtractionProcessor.GotResource)
                        return UnitPathType.Collect_Resource;
                    break;
                case UnitPathType.Storage_Resource:
                    if (target.TargetType == UnitTargetType.Construction &&
                        target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return UnitPathType.Storage_Resource;
                    break;
                case UnitPathType.Attack:
                    if (!target.IsAnyNull() && target.Affiliation != Affiliation && target.CastPossible<IDamagable>() 
                        || _attackProcessor.CheckEnemiesInAttackZone())
                        return UnitPathType.Attack;
                    break;
                case UnitPathType.HideInConstruction:
                    if (target.Affiliation == Affiliation && target.TryCast(out IHiderConstruction hiderConstruction) 
                                                          && hiderConstruction.Hider.CheckAccess(Unit.UnitType))
                        return UnitPathType.HideInConstruction;
                    break;
            }
            
            return UnitPathType.Move;
        }
    }
}