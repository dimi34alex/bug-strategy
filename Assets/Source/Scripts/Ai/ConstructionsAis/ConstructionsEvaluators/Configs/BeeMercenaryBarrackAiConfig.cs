using UnityEngine;

namespace BugStrategy.Ai.ConstructionsAis
{
    [CreateAssetMenu(fileName = nameof(BeeMercenaryBarrackAiConfig), menuName = "Configs/Ai/Constructions/" + nameof(BeeMercenaryBarrackAiConfig))]
    public class BeeMercenaryBarrackAiConfig : ConstructionAiConfigBase
    {
        [SerializeField] private RecruitingEvaluationConfig murmurs;
        [SerializeField] private RecruitingEvaluationConfig horntails;
        [SerializeField] private RecruitingEvaluationConfig sawyers;
        
        public RecruitingEvaluationConfig Murmurs => murmurs;
        public RecruitingEvaluationConfig Horntails => horntails;
        public RecruitingEvaluationConfig Sawyers => sawyers;
    }
}