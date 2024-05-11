using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public sealed class RecruitBumblebeeEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Bumblebee;

        public RecruitBumblebeeEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourceGlobalStorage)
        {
        }
    }
}