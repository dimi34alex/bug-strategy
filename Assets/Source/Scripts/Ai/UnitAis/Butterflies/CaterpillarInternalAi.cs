using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Missions;
using BugStrategy.Unit.Butterflies;

namespace BugStrategy.Ai.UnitAis
{
    public class CaterpillarInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }
        
        public CaterpillarInternalAi (Caterpillar caterpillar, IEnumerable<EntityStateBase> states, MissionData missionData)
            : base(caterpillar, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new ResourceCollectOrderEvaluator(caterpillar, this, caterpillar.ResourceExtractionProcessor, missionData),
                new BuildOrderEvaluator(caterpillar, this),
                new HideInConstructionOrderEvaluator(caterpillar, this),
                new StorageResourceStateOrderEvaluator(caterpillar, this, caterpillar.ResourceExtractionProcessor)
            };
        }
    }
}