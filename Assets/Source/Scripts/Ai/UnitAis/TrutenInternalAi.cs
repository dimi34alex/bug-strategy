using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class TrutenInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public TrutenInternalAi(Truten truten, IEnumerable<EntityStateBase> states) 
            : base(truten, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(truten, this),
                new AttackOrderEvaluator(truten, this, truten.AttackProcessor)
            };
        }
    }
}