using System.Collections.Generic;
using Construction.TownHalls;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.RecruitingEvaluators;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Source.Scripts.Ai.ConstructionsAis
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