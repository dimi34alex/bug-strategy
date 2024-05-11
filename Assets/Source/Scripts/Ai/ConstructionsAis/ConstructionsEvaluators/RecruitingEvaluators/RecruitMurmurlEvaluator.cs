using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public class RecruitMurmurEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Murmur;
    
        public RecruitMurmurEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourceGlobalStorage)
        {
            
        }
    }
}