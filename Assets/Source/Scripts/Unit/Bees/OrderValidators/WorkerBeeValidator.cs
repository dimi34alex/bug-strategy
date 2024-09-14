using BugStrategy.Constructions;
using BugStrategy.Libs;
using BugStrategy.ResourceSources;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;

namespace BugStrategy.Unit.Bees
{
    public class WorkerBeeValidator : OrderValidatorBase
    {
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public WorkerBeeValidator(UnitBase unit, float interactionRange,  ResourceExtractionProcessor resourceExtractionProcessor)
            : base(unit, interactionRange)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }

        protected override UnitPathData ValidateAutoOrder(IUnitTarget target)
        {
            if(target.IsAnyNull() || !target.IsActive)
                return new UnitPathData(null, UnitPathType.Move);
            
            switch (target.TargetType)
            {
                case UnitTargetType.ResourceSource:
                    if(!_resourceExtractionProcessor.GotResource && 
                       target.Cast<ResourceSourceBase>().CanBeCollected)
                        return new UnitPathData(target, UnitPathType.Collect_Resource);
                    break;
                case UnitTargetType.Construction:
                    if (target.CastPossible<BuildingProgressConstruction>())
                        return new UnitPathData(target, UnitPathType.Build_Construction);
                
                    if (target.Affiliation == Affiliation &&
                        target.CastPossible<TownHallBase>() &&
                        _resourceExtractionProcessor.GotResource)
                        return new UnitPathData(target, UnitPathType.Storage_Resource);

                    if (target.Affiliation == Affiliation && target.TryCast(out IHiderConstruction hiderConstruction) 
                                                          && hiderConstruction.Hider.CheckAccess(Unit.UnitType))
                        return new UnitPathData(target, UnitPathType.HideInConstruction);

                    break;
            }
            
            return new UnitPathData(null, UnitPathType.Move);
        }
        
        protected override UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType)
        {
            if (target.IsAnyNull() || !target.IsActive) 
                return UnitPathType.Move;
            
            switch (pathType)
            {
                case UnitPathType.Build_Construction:
                    if (target.TargetType == UnitTargetType.Construction &&
                        //TODO: create ants constructions and start check affiliations
                        // unitTarget.Affiliation == Affiliation &&
                        target.CastPossible<BuildingProgressConstruction>())
                        return UnitPathType.Build_Construction;
                    break;
                case UnitPathType.Repair_Construction:
                    if (target.TargetType == UnitTargetType.Construction &&
                        target.Affiliation == Affiliation &&
                        target.CastPossible<ConstructionBase>())
                        return UnitPathType.Repair_Construction;
                    break;
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
                case UnitPathType.Switch_Profession:
                    if (Unit.Fraction == FractionType.Ants &&
                        target.TargetType == UnitTargetType.Construction)
                        // TODO: create construction for switching ants professions
                        return UnitPathType.Switch_Profession;
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