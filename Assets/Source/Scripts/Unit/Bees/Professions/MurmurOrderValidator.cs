using Construction.TownHalls;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class MurmurOrderValidator : OrderValidatorBase
    {
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        private readonly CooldownProcessor _cooldownProcessor;
        private readonly AttackProcessorBase _attackProcessor;
        
        public MurmurOrderValidator(UnitBase unit, float interactionRange, AttackProcessorBase attackProcessor, 
            CooldownProcessor cooldownProcessor, ResourceExtractionProcessor resourceExtractionProcessor) 
            : base(unit, interactionRange)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
            _cooldownProcessor = cooldownProcessor;
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
                    Debug.Log("ResourceSource");
                    if (!_resourceExtractionProcessor.GotResource)
                    {
                        Debug.Log("Collect_Resource");
                        return new UnitPathData(target, UnitPathType.Collect_Resource);
                    }
                    break;
                case UnitTargetType.Construction:
                    if(target.Affiliation != Affiliation)
                        return new UnitPathData(target, UnitPathType.Attack);

                    if (target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return new UnitPathData(target, UnitPathType.Storage_Resource);  
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
                        target.CastPossible<ResourceSourceBase>() &&
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
            }
            
            return UnitPathType.Move;
        }
    }
}