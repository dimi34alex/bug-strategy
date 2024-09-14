using UnityEngine;

namespace BugStrategy.Ai.ConstructionsAis
{
    [CreateAssetMenu(fileName = nameof(BeeSiegeWeaponsBarrackAiConfig), menuName = "Configs/Ai/Constructions/" + nameof(BeeSiegeWeaponsBarrackAiConfig))]
    public class BeeSiegeWeaponsBarrackAiConfig : ConstructionAiConfigBase
    {
        [SerializeField] private RecruitingEvaluationConfig mobileHive;
        [SerializeField] private RecruitingEvaluationConfig honeyCatapult;
        
        public RecruitingEvaluationConfig MobileHive => mobileHive;
        public RecruitingEvaluationConfig HoneyCatapult => honeyCatapult;
    }
}