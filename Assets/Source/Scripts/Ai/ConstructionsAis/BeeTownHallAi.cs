using System.Collections.Generic;
using BugStrategy.Constructions.BeeTownHall;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class BeeTownHallAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }
        
        public BeeTownHallAi(UnitsAiRepository unitsAiRepository, BeeTownHall townHall, BeeTownHallAiConfig config,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
            : base(townHall)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitWorkerBeeEvaluator(unitsAiRepository, townHall, config.WorkerBees, teamsResourcesGlobalStorage)
            };
        }
    }
}