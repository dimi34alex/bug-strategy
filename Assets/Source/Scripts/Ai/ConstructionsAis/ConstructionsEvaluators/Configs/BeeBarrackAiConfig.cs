using UnityEngine;

namespace BugStrategy.Ai.ConstructionsAis
{
    [CreateAssetMenu(fileName = nameof(BeeBarrackAiConfig), menuName = "Configs/Ai/Constructions/" + nameof(BeeBarrackAiConfig))]
    public class BeeBarrackAiConfig : ConstructionAiConfigBase
    {
        [SerializeField] private RecruitingEvaluationConfig wasps;
        [SerializeField] private RecruitingEvaluationConfig bumblebees;
        [SerializeField] private RecruitingEvaluationConfig hornets;
        [SerializeField] private RecruitingEvaluationConfig trutens;
        
        public RecruitingEvaluationConfig Wasps => wasps;
        public RecruitingEvaluationConfig Bumblebees => bumblebees;
        public RecruitingEvaluationConfig Hornets => hornets;
        public RecruitingEvaluationConfig Trutens => trutens;
    }
}