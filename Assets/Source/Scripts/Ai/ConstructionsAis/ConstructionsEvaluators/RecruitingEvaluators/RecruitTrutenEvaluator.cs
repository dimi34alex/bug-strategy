using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class RecruitTrutenEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Truten;

        public RecruitTrutenEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourcesGlobalStorage)
        {
        }
    }
}