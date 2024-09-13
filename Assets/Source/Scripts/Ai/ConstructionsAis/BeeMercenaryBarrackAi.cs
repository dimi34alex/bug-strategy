using System.Collections.Generic;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.RecruitingEvaluators;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;

namespace Source.Scripts.Ai.ConstructionsAis
{
    public class BeeMercenaryBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeMercenaryBarrackAi(UnitsAiRepository unitsAiRepository, BeeMercenaryBarrack barrack, 
            BeeMercenaryBarrackAiConfig config, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitMurmurEvaluator(unitsAiRepository, barrack, config.Murmurs, teamsResourcesGlobalStorage),
                new RecruitHorntailEvaluator(unitsAiRepository, barrack, config.Horntails, teamsResourcesGlobalStorage),
                new RecruitSawyerEvaluator(unitsAiRepository, barrack, config.Sawyers, teamsResourcesGlobalStorage)
            };
        }
    }
}