using System;
using BugStrategy.Constructions;
using BugStrategy.Constructions.BuildProgressConstructions;
using BugStrategy.Libs;
using BugStrategy.Unit;
using CycleFramework.Extensions;

namespace BugStrategy.Ai.InternalAis
{
    public class BuildOrderEvaluator : UnitInternalEvaluator
    {
        private BuildingProgressConstruction _hashedBuildingProgressConstruction;
        
        public BuildOrderEvaluator(UnitBase unit, InternalAiBase internalAi) 
            : base(unit, internalAi)
        {
            
        }
        
        public override float Evaluate()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (Unit.CurrentPathData.Target.TryCast(out _hashedBuildingProgressConstruction) && 
                        _hashedBuildingProgressConstruction.BuildingProgressState != BuildingProgressState.Completed)
                        return 1;
                    break;
                case AiUnitStateType.BuildConstruction:
                    if (UnitInternalAi.GlobalAiOrderTarget.IsAnyNull())
                        throw new NullReferenceException($"Target is null");
                    return 1;
            }

            return float.MinValue;
        }

        public override void Apply()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.BuildConstruction:
                    if (UnitInternalAi.GlobalAiOrderTarget.IsAnyNull())
                        throw new NullReferenceException($"Target is null");
                    Unit.HandleGiveOrder(UnitInternalAi.GlobalAiOrderTarget, UnitPathType.Build_Construction);
                    break;
            }
            
            Unit.HandleGiveOrder(_hashedBuildingProgressConstruction, UnitPathType.Build_Construction);
        }
    }
}