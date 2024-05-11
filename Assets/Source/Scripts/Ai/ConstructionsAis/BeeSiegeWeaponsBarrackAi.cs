using System.Collections.Generic;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public class BeeSiegeWeaponsBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeSiegeWeaponsBarrackAi(UnitsAiRepository unitsAiRepository, BeeSiegeWeaponsBarrack barrack, 
            BeeSiegeWeaponsBarrackAiConfig config, TeamsResourceGlobalStorage teamsResourceGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitMobileHiveEvaluator(unitsAiRepository, barrack, config.MobileHive, teamsResourceGlobalStorage),
                new RecruitHoneyCatapultEvaluator(unitsAiRepository, barrack, config.HoneyCatapult, teamsResourceGlobalStorage)
            };
        }
    }
}