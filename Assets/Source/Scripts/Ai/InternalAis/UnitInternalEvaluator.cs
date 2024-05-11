namespace Source.Scripts.Ai.InternalAis
{
    public abstract class UnitInternalEvaluator : EvaluatorBase
    {
        protected readonly UnitBase Unit;
        protected readonly InternalAiBase UnitInternalAi;
        
        protected UnitInternalEvaluator(UnitBase unit, InternalAiBase internalAi)
        {
            Unit = unit;
            UnitInternalAi = internalAi;
        }
    }
}