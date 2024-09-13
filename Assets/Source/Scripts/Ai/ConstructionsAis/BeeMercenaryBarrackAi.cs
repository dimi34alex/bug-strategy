using System.Collections.Generic;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.RecruitingEvaluators;

namespace Source.Scripts.Ai.ConstructionsAis
{
    public class BeeMercenaryBarrackAi : ConstructionAiBase
    {
        protected override List<ConstructionEvaluatorBase> Evaluators { get; }

        public BeeMercenaryBarrackAi(UnitsAiRepository unitsAiRepository, BeeMercenaryBarrack barrack, 
            BeeMercenaryBarrackAiConfig config, TeamsResourceGlobalStorage teamsResourceGlobalStorage) 
            : base(barrack)
        {
            Evaluators = new List<ConstructionEvaluatorBase>()
            {
                new RecruitMurmurEvaluator(unitsAiRepository, barrack, config.Murmurs, teamsResourceGlobalStorage),
                new RecruitHorntailEvaluator(unitsAiRepository, barrack, config.Horntails, teamsResourceGlobalStorage),
                new RecruitSawyerEvaluator(unitsAiRepository, barrack, config.Sawyers, teamsResourceGlobalStorage)
            };
        }
    }
}