using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees;

namespace Source.Scripts.Ai.UnitAis
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