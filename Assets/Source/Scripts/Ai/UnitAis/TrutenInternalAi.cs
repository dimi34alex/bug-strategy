using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees;

namespace Source.Scripts.Ai.UnitAis
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