using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class WaspInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }
        
        public WaspInternalAi(Wasp wasp, IEnumerable<EntityStateBase> states)
            : base(wasp, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(wasp, this),
                new AttackOrderEvaluator(wasp, this, wasp.AttackProcessor)
            };
        }
    }
}