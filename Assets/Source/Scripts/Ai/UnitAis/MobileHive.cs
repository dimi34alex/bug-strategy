using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees;

namespace Source.Scripts.Ai.UnitAis
{
    public class MobileHiveInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public MobileHiveInternalAi(MobileHive mobileHive, IEnumerable<EntityStateBase> states) 
            : base(mobileHive, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(mobileHive, this),
                new AttackOrderEvaluator(mobileHive, this, mobileHive.AttackProcessor)
            };
        }
    }
}