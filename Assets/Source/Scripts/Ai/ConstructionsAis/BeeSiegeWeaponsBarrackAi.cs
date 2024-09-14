using System.Collections.Generic;
using BugStrategy.Constructions.BeeSiegeWeaponsBarrack;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;

namespace BugStrategy.Ai.ConstructionsAis
{
    public class BeeSiegeWeaponsBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeSiegeWeaponsBarrackAi(UnitsAiRepository unitsAiRepository, BeeSiegeWeaponsBarrack barrack, 
            BeeSiegeWeaponsBarrackAiConfig config, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitMobileHiveEvaluator(unitsAiRepository, barrack, config.MobileHive, teamsResourcesGlobalStorage),
                new RecruitHoneyCatapultEvaluator(unitsAiRepository, barrack, config.HoneyCatapult, teamsResourcesGlobalStorage)
            };
        }
    }
}