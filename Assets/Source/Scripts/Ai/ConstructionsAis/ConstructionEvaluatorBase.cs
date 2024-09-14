using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Ai.ConstructionsAis
{
    public abstract class ConstructionEvaluatorBase : EvaluatorBase
    {
        protected readonly ITeamsResourcesGlobalStorage TeamsResourcesGlobalStorage;

        protected ConstructionEvaluatorBase(ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage)
        {
            TeamsResourcesGlobalStorage = teamsResourcesGlobalStorage;
        }
    }
}