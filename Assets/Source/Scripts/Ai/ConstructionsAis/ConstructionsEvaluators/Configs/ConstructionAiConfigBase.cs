using UnityEngine;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class ConstructionAiConfigBase : ScriptableObject
    {
        [SerializeField] private float evaluationPause;

        public float EvaluationPause => evaluationPause;
    }
}