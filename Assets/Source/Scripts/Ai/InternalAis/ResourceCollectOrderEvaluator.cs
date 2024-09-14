using BugStrategy.Missions;
using BugStrategy.ResourceSources;
using BugStrategy.Unit;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;

namespace BugStrategy.Ai.InternalAis
{
    public class ResourceCollectOrderEvaluator : UnitInternalEvaluator
    {
        private readonly IReadOnlyResourceExtractionProcessor _resourceExtractionProcessor;
        private readonly MissionData _missionData;
        private ResourceSourceBase _hashedResourceSource;
        
        public ResourceCollectOrderEvaluator(UnitBase unit, InternalAiBase internalAi
            , IReadOnlyResourceExtractionProcessor resourceExtractionProcessor, MissionData missionData) 
            : base(unit, internalAi)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
            _missionData = missionData;
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
            }
            
            return float.MinValue;
        }

        public override void Apply()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.CollectResource:
                    if(_hashedResourceSource == null || !_hashedResourceSource.CanBeCollected)
                        _hashedResourceSource = _missionData.ResourceSourcesRepository
                            .GetNearest(Unit.transform.position, true);
                    break;
            }
            
            Unit.HandleGiveOrder(_hashedResourceSource, UnitPathType.Collect_Resource);
        }
    }
}