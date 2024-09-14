using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;

namespace BugStrategy.Ai.ConstructionsAis
{
    public sealed class RecruitMobileHiveEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.MobileHive;

        public RecruitMobileHiveEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourcesGlobalStorage)
        {
        }
    }
}