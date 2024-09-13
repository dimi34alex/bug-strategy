using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Bees;

namespace Source.Scripts.Ai.UnitAis
{
    public class HorntailInternalAi : InternalAiBase
    {
        protected override List<UnitInternalEvaluator> InternalEvaluators { get; }

        public HorntailInternalAi(Horntail horntail, IEnumerable<EntityStateBase> states) 
            : base(horntail, states)
        {
            InternalEvaluators = new List<UnitInternalEvaluator>()
            {
                new HideInConstructionOrderEvaluator(horntail, this),
                new AttackOrderEvaluator(horntail, this, horntail.AttackProcessor)
            };
        }
    }
}