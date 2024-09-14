using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class HoneyCatapultInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public HoneyCatapultInternalAi(HoneyCatapult honeyCatapult, IEnumerable<EntityStateBase> states) 
            : base(honeyCatapult, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(honeyCatapult, this),
                new AttackOrderEvaluator(honeyCatapult, this, honeyCatapult.AttackProcessor)
            };
        }
    }
}