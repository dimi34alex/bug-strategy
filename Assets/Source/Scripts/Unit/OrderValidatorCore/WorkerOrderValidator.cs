﻿using BugStrategy.Constructions;
using BugStrategy.Constructions.BuildProgressConstructions;
using BugStrategy.Libs;
using BugStrategy.ResourceSources;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.OrderValidatorCore
{
    public sealed class WorkerOrderValidator : OrderValidatorBase
    {
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public WorkerOrderValidator(UnitBase unit, float interactionRange,  ResourceExtractionProcessor resourceExtractionProcessor)
            : base(unit, interactionRange)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }

        protected override UnitPathData ValidateAutoOrder(ITarget target)
        {
            if(target.IsAnyNull())
                return new UnitPathData(null, UnitPathType.Move);
            
            switch (target.TargetType)
            {
                case TargetType.ResourceSource:
                    if(!_resourceExtractionProcessor.GotResource && 
                       target.Cast<ResourceSourceBase>().CanBeCollected)
                        return new UnitPathData(target, UnitPathType.Collect_Resource);
                    break;
                case TargetType.Construction:
                    if (target.CastPossible<BuildingProgressConstruction>())
                        return new UnitPathData(target, UnitPathType.Build_Construction);
                
                    if (target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return new UnitPathData(target, UnitPathType.Storage_Resource);  
                    break;                
            }
            
            return new UnitPathData(null, UnitPathType.Move);
        }
        
        protected override UnitPathType ValidateHandleOrder(ITarget target, UnitPathType pathType)
        {
            if (target.IsAnyNull()) 
                return UnitPathType.Move;
            
            switch (pathType)
            {
                case UnitPathType.Build_Construction:
                    if (target.TargetType == TargetType.Construction &&
                        target.Affiliation == Affiliation &&
                        target.CastPossible<BuildingProgressConstruction>())
                        return UnitPathType.Build_Construction;
                    break;
                case UnitPathType.Repair_Construction:
                    if (target.TargetType == TargetType.Construction &&
                        target.Affiliation == Affiliation &&
                        target.CastPossible<ConstructionBase>())
                        return UnitPathType.Repair_Construction;
                    break;
                case UnitPathType.Collect_Resource:
                    if (target.TargetType == TargetType.ResourceSource &&
                        target.Cast<ResourceSourceBase>().CanBeCollected &&
                        !_resourceExtractionProcessor.GotResource)
                        return UnitPathType.Collect_Resource;
                    break;
                case UnitPathType.Storage_Resource:
                    if (target.TargetType == TargetType.Construction &&
                        target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return UnitPathType.Storage_Resource;
                    break;
                case UnitPathType.Switch_Profession:
                    if (Unit.Fraction == FractionType.Ants &&
                        target.TargetType == TargetType.Construction &&
                        target.CastPossible<AntWorkshopBase>())
                        return UnitPathType.Switch_Profession;
                    break;
            }

            return UnitPathType.Move;
        }
    }
}
