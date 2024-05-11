using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
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