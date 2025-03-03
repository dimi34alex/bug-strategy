using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;
using BugStrategy.Unit.RecruitingSystem;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class RecruitSawyerEvaluator : RecruitEvaluatorBase
    {
        protected override UnitType RecruitUnitType => UnitType.Sawyer;
    
        public RecruitSawyerEvaluator(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
            RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
            : base(unitsAiRepository, recruitingConstruction, recruitingEvaluationConfig, teamsResourcesGlobalStorage)
        {
            
        }
    }
}