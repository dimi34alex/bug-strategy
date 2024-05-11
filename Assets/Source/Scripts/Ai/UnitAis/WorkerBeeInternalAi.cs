using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
{
    public class WorkerBeeInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }
        
        public WorkerBeeInternalAi(WorkerBee workerBee, IEnumerable<EntityStateBase> states)
            : base(workerBee, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new ResourceCollectOrderEvaluator(workerBee, this, workerBee.ResourceExtractionProcessor),
                new BuildOrderEvaluator(workerBee, this),
                new HideInConstructionOrderEvaluator(workerBee, this),
                new StorageResourceStateOrderEvaluator(workerBee, this, workerBee.ResourceExtractionProcessor)
            };
        }
    }
}