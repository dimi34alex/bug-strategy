using BugStrategy.Unit;
using BugStrategy.UnitsHideCore;
using CycleFramework.Extensions;

namespace BugStrategy.Ai.InternalAis
{
    public class HideInConstructionOrderEvaluator : UnitInternalEvaluator
    {
        private ITarget _hashedTarget;
        
        public HideInConstructionOrderEvaluator(UnitBase unit, InternalAiBase internalAi) 
            : base(unit, internalAi)
        {
        }

        public override float Evaluate()
        {
            float priorityScale = Unit.HealthStorage.Capacity/Unit.HealthStorage.CurrentValue;
            if (UnitInternalAi.UnitTookDamage)
                priorityScale *= 1.5f;
            
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (Unit.CurrentPathData.PathType == UnitPathType.HideInConstruction && 
                        Unit.CurrentPathData.Target.CastPossible<IHiderConstruction>())
                    {
                        _hashedTarget = Unit.CurrentPathData.Target;
                        return 1 * 5 * priorityScale;
                    }
                    break;
                case AiUnitStateType.HideInConstruction:
                    if (UnitInternalAi.GlobalAiOrderTarget.CastPossible<IHiderConstruction>())
                    {
                        _hashedTarget = UnitInternalAi.GlobalAiOrderTarget;
                        return 1 * 5 * priorityScale;
                    }
                    break;
            }

            return float.MinValue;
        }

        public override void Apply()
        {
            Unit.HandleGiveOrder(_hashedTarget, UnitPathType.HideInConstruction);
        }
    }
}