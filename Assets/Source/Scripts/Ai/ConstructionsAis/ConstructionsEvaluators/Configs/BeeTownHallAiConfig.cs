using UnityEngine;

namespace BugStrategy.Ai.ConstructionsAis
{
    [CreateAssetMenu(fileName = nameof(BeeTownHallAiConfig), menuName =  "Configs/Ai/Constructions/" + nameof(BeeTownHallAiConfig))]
    public class BeeTownHallAiConfig : ConstructionAiConfigBase
    {
        [field: SerializeField] public RecruitingEvaluationConfig WorkerBees { get; private set; }
    }
}