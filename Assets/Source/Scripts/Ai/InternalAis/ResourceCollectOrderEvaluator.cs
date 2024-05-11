using Unit.ProcessorsCore;

namespace Source.Scripts.Ai.InternalAis
{
    public class ResourceCollectOrderEvaluator : UnitInternalEvaluator
    {
        private readonly IReadOnlyResourceExtractionProcessor _resourceExtractionProcessor;
        private ResourceSourceBase _hashedResourceSource;
        
        public ResourceCollectOrderEvaluator(UnitBase unit, InternalAiBase internalAi
            , IReadOnlyResourceExtractionProcessor resourceExtractionProcessor) 
            : base(unit, internalAi)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }
        
        public override float Evaluate()
        {
            if (_resourceExtractionProcessor.GotResource)
                return float.MinValue;
            
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (Unit.CurrentPathData.Target.TryCast(out _hashedResourceSource) && 
                        _hashedResourceSource.CanBeCollected)
                        return 1;
                    if (_resourceExtractionProcessor.PrevResourceSource != null &&
                        _resourceExtractionProcessor.PrevResourceSource.CanBeCollected)
                    {
                        _hashedResourceSource = _resourceExtractionProcessor.PrevResourceSource;
                        return 1;
                    }
                    break;
                case AiUnitStateType.CollectResource:
                    return 5;
                    break;
            }
            
            return float.MinValue;
        }

        public override void Apply()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.CollectResource:
                    if(_hashedResourceSource == null || !_hashedResourceSource.CanBeCollected)
                        _hashedResourceSource = FrameworkCommander.GlobalData.ResourceSourcesRepository
                            .GetNearest(Unit.transform.position, true);
                    break;
            }
            
            Unit.HandleGiveOrder(_hashedResourceSource, UnitPathType.Collect_Resource);
        }
    }
}