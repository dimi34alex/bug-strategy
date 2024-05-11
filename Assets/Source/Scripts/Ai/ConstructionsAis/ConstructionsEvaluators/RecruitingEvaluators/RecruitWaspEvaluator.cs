using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public sealed class RecruitWaspEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Wasp;
        
        public RecruitWaspEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourceGlobalStorage)
        {
            
        }
    }
}