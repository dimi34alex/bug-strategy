using System.Collections.Generic;
using BugStrategy.Ai.InternalAis;
using BugStrategy.EntityState;
using BugStrategy.Unit.Bees;

namespace BugStrategy.Ai.UnitAis
{
    public class MurmurInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public MurmurInternalAi(Murmur murmur, IEnumerable<EntityStateBase> states) 
            : base(murmur, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(murmur, this),
                new AttackOrderEvaluator(murmur, this, murmur.AttackProcessor)
            };
        }
    }
}