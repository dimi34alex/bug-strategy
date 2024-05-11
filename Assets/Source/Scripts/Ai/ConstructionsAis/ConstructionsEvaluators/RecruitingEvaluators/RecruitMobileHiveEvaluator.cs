using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public sealed class RecruitMobileHiveEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.MobileHive;

        public RecruitMobileHiveEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourceGlobalStorage)
        {
        }
    }
}