using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class HorntailInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public HorntailInternalAi(Horntail horntail, IEnumerable<EntityStateBase> states) 
            : base(horntail, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(horntail, this),
                new AttackOrderEvaluator(horntail, this, horntail.AttackProcessor)
            };
        }
    }
}