using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees;

namespace Source.Scripts.Ai.UnitAis
{
    public class SawyerInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public SawyerInternalAi(Sawyer sawyer, IEnumerable<EntityStateBase> states) 
            : base(sawyer, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(sawyer, this),
                new AttackOrderEvaluator(sawyer, this, sawyer.AttackProcessor)
            };
        }
    }
}