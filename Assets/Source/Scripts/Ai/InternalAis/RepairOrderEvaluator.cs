using System;
using BugStrategy.Constructions;
using BugStrategy.Constructions.BuildProgressConstructions;
using BugStrategy.Libs;
using BugStrategy.Unit;
using CycleFramework.Extensions;

namespace BugStrategy.Ai.InternalAis
{
    public class RepairOrderEvaluator : UnitInternalEvaluator
    {
        private ConstructionBase _hashedConstruction;
        
        public RepairOrderEvaluator(UnitBase unit, InternalAiBase internalAi) 
            : base(unit, internalAi)
        {
            
        }
        
        public override float Evaluate()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (Unit.CurrentPathData.Target.TryCast(out _hashedConstruction) && 
                        Unit.CurrentPathData.PathType == UnitPathType.Repair_Construction)
                        return 1;
                    break;
                case AiUnitStateType.RepairConstruction:
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
                case AiUnitStateType.RepairConstruction:
                    if (UnitInternalAi.GlobalAiOrderTarget.IsAnyNull())
                        throw new NullReferenceException($"Target is null");
                    Unit.HandleGiveOrder(UnitInternalAi.GlobalAiOrderTarget, UnitPathType.Repair_Construction);
                    return;
            }
            
            Unit.HandleGiveOrder(_hashedConstruction, UnitPathType.Repair_Construction);
        }
    }
}