using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
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