using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;

namespace BugStrategy.Ai.ConstructionsAis
{
    public sealed class RecruitHoneyCatapultEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.HoneyCatapult;

        public RecruitHoneyCatapultEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourcesGlobalStorage)
        {
        }
    }
}