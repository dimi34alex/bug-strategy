using System.Collections.Generic;
using Unit.Bees;

namespace Source.Scripts.Ai.InternalAis
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