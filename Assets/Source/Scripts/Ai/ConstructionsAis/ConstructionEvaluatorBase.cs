namespace Source.Scripts.Ai.ConstructionsAis
{
    public abstract class ConstructionEvaluatorBase : EvaluatorBase
    {
        protected readonly TeamsResourceGlobalStorage TeamsResourceGlobalStorage;

        protected ConstructionEvaluatorBase(TeamsResourceGlobalStorage teamsResourceGlobalStorage)
        {
            TeamsResourceGlobalStorage = teamsResourceGlobalStorage;
        }
    }
}