using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
{
    public class BumblebeeInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public BumblebeeInternalAi(Bumblebee bumblebee, IEnumerable<EntityStateBase> states) 
            : base(bumblebee, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(bumblebee, this),
                new AttackOrderEvaluator(bumblebee, this, bumblebee.AttackProcessor)
            };
        }
    }
}