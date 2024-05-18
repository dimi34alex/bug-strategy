using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
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