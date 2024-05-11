using System;
using System.Collections.Generic;
using CustomTimer;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;

namespace Source.Scripts.Ai.ConstructionsAis
{
    public abstract class ConstructionAiBase
    {
        private readonly Timer _evaluatePause;

        protected abstract List<ConstructionEvaluatorBase> Evaluators { get; }

        public event Action<ConstructionAiBase> ConstructionDestructed; 
        
        protected ConstructionAiBase(ConstructionBase constructionBase)
        {
            constructionBase.OnDestruction += () => { ConstructionDestructed?.Invoke(this); };
            
            _evaluatePause = new Timer(5f);
            _evaluatePause.OnTimerEnd += Evaluate;
        }

        public void HandleUpdate(float deltaTime)
        {
            _evaluatePause.Tick(deltaTime);
        }
        
        private void Evaluate()
        {
            _evaluatePause.Reset();
            
            var priority = float.MinValue;
            ConstructionEvaluatorBase evaluator = null;
            
            foreach (var internalEvaluator in Evaluators)
            {
                var newPriority = internalEvaluator.Evaluate();

                if (newPriority > 0 && newPriority > priority)
                {
                    priority = newPriority;
                    evaluator = internalEvaluator;
                }
            }

            evaluator?.Apply();
        }
    }
}