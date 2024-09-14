using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class HornetInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public HornetInternalAi(Hornet hornet, IEnumerable<EntityStateBase> states) 
            : base(hornet, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(hornet, this),
                new AttackOrderEvaluator(hornet, this, hornet.AttackProcessor)
            };
        }
    }
}