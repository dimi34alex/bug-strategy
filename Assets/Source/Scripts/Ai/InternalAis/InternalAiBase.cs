using System.Collections.Generic;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Ai.InternalAis
{
    public abstract class InternalAiBase
    {
        protected readonly UnitBase Unit; 
        
        protected abstract List<UnitInternalEvaluator> InternalEvaluators { get; }
        public ITarget GlobalAiOrderTarget { get; protected set; }
        public AiUnitStateType AiState { get; protected set; }
        public bool UnitTookDamage { get; protected set; }
        public ITarget Attacker { get; protected set; }
        
        protected InternalAiBase(UnitBase unit, IEnumerable<EntityStateBase> states)
        {
            Unit = unit;
            Unit.TookDamage += () => UnitTookDamage = true;
            Unit.PathTargetDeactivated += TryGiveOrder;
            Unit.TookDamageWithAttacker += attacker =>
            {
                Attacker = attacker;
                TryGiveOrder();
                Attacker = null;
            }; 
            
            foreach (var stateBase in states)
                stateBase.StateExecuted += TryGiveOrder;
        }
        
        public void SetOrderPriority(ITarget target, AiUnitStateType aiUnitStateType)
        {
            if (!GlobalAiOrderTarget.IsAnyNull()) 
                GlobalAiOrderTarget.OnDeactivation -= SetOrderPriority;

            GlobalAiOrderTarget = target;
            AiState = aiUnitStateType;

            if (!GlobalAiOrderTarget.IsAnyNull()) 
                GlobalAiOrderTarget.OnDeactivation += SetOrderPriority;

            TryGiveOrder();
        }

        private void SetOrderPriority(ITarget _)
            => SetOrderPriority(null, AiState);
        
        protected void TryGiveOrder()
        {
            var priority = float.MinValue;
            UnitInternalEvaluator evaluator = null;
            foreach (var internalEvaluator in InternalEvaluators)
            {
                var newPriority = internalEvaluator.Evaluate();

                if (newPriority > priority && newPriority > 0)
                {
                    priority = newPriority;
                    evaluator = internalEvaluator;
                }
            }

            UnitTookDamage = false;
            if (evaluator == null)
            {
                if (!GlobalAiOrderTarget.IsAnyNull()) 
                    GlobalAiOrderTarget.OnDeactivation -= SetOrderPriority;
                GlobalAiOrderTarget = null;
                
                if(AiState != AiUnitStateType.Auto)
                    AiState = AiUnitStateType.Idle;
                
                Unit.AutoGiveOrder(null);
                return;
            }
            
            if(Unit.UnitType == UnitType.Murmur)
                Debug.Log($"{nameof(GetType)} || {evaluator.GetType()} || {evaluator}");
            evaluator.Apply();
        }
        
        public void Reset()
        {
            if (!GlobalAiOrderTarget.IsAnyNull())
                GlobalAiOrderTarget.OnDeactivation -= SetOrderPriority;
            GlobalAiOrderTarget = null;
            AiState = AiUnitStateType.Auto;
            UnitTookDamage = false;
        }
    }
}