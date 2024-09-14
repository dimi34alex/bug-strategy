using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;

namespace BugStrategy.Ai.ConstructionsAis
{
    public sealed class RecruitBumblebeeEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Bumblebee;

        public RecruitBumblebeeEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourcesGlobalStorage)
        {
        }
    }
}