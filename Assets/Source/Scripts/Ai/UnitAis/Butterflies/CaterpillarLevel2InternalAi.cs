using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Missions;
using BugStrategy.Unit.Butterflies;

namespace BugStrategy.Ai.UnitAis
{
    public class CaterpillarLevel2InternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }
        
        public CaterpillarLevel2InternalAi (CaterpillarLevel2 caterpillarLevel2, IEnumerable<EntityStateBase> states, MissionData missionData)
            : base(caterpillarLevel2, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new ResourceCollectOrderEvaluator(caterpillarLevel2, this, caterpillarLevel2.ResourceExtractionProcessor, missionData),
                new BuildOrderEvaluator(caterpillarLevel2, this),
                new HideInConstructionOrderEvaluator(caterpillarLevel2, this),
                new StorageResourceStateOrderEvaluator(caterpillarLevel2, this, caterpillarLevel2.ResourceExtractionProcessor)
            };
        }
    }
}