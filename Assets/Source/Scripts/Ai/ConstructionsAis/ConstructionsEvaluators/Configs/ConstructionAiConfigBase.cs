using UnityEngine;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs
{
    public class ConstructionAiConfigBase : ScriptableObject
    {
        [SerializeField] private float evaluationPause;

        public float EvaluationPause => evaluationPause;
    }
}