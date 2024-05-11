using System.Collections.Generic;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.ConstructionsAis
{
    public class BeeBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeBarrackAi(UnitsAiRepository unitsAiRepository, BeeBarrack barrack, 
            BeeBarrackAiConfig config, TeamsResourceGlobalStorage teamsResourceGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitWaspEvaluator(unitsAiRepository, barrack, config.Wasps, teamsResourceGlobalStorage),
                new RecruitBumblebeeEvaluator(unitsAiRepository, barrack, config.Bumblebees, teamsResourceGlobalStorage),
                new RecruitHornetEvaluator(unitsAiRepository, barrack, config.Hornets, teamsResourceGlobalStorage),
                new RecruitTrutenEvaluator(unitsAiRepository, barrack, config.Trutens, teamsResourceGlobalStorage)
            };
        }
    }
}