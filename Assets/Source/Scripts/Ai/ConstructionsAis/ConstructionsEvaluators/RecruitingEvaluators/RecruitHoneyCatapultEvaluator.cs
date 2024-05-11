using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public sealed class RecruitHoneyCatapultEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.HoneyCatapult;

        public RecruitHoneyCatapultEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourceGlobalStorage)
        {
        }
    }
}