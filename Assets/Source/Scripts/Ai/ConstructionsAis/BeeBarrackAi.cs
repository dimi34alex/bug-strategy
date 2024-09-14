using System.Collections.Generic;
using BugStrategy.Constructions.BeeBarrack;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class BeeBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeBarrackAi(UnitsAiRepository unitsAiRepository, BeeBarrack barrack, 
            BeeBarrackAiConfig config, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitWaspEvaluator(unitsAiRepository, barrack, config.Wasps, teamsResourcesGlobalStorage),
                new RecruitBumblebeeEvaluator(unitsAiRepository, barrack, config.Bumblebees, teamsResourcesGlobalStorage),
                new RecruitHornetEvaluator(unitsAiRepository, barrack, config.Hornets, teamsResourcesGlobalStorage),
                new RecruitTrutenEvaluator(unitsAiRepository, barrack, config.Trutens, teamsResourcesGlobalStorage)
            };
        }
    }
}