using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Source.Scripts.Ai.ConstructionsAis
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