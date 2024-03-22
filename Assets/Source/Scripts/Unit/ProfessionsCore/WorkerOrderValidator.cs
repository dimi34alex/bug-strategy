using Construction.TownHalls;
using Unit.ProfessionsCore.Processors;

namespace Unit.ProfessionsCore
{
    public sealed class WorkerOrderValidator : OrderValidatorBase
    {
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public WorkerOrderValidator(UnitBase unit, float interactionRange,  ResourceExtractionProcessor resourceExtractionProcessor)
            : base(unit, interactionRange)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }

        protected override UnitPathData ValidateAutoOrder(IUnitTarget target)
        {
            if(target.IsAnyNull())
                return new UnitPathData(null, UnitPathType.Move);
            
            switch (target.TargetType)
            {
                case UnitTargetType.ResourceSource:
                    if(!_resourceExtractionProcessor.GotResource)
                        return new UnitPathData(target, UnitPathType.Collect_Resource);
                    break;
                case UnitTargetType.Construction:
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
        
        protected override UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType)
        {
            if (target.IsAnyNull()) 
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
                case UnitPathType.Switch_Profession:
                    if (Affiliation == AffiliationEnum.Ants &&
                        target.TargetType == UnitTargetType.Construction)
                        // TODO: create construction for switching ants professions
                        return UnitPathType.Switch_Profession;
                    break;
            }

            return UnitPathType.Move;
        }
    }
}
