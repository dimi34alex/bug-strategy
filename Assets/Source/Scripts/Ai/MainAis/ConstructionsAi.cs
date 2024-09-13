using System;
using System.Collections.Generic;
using Constructions;
using Source.Scripts.Ai.ConstructionsAis;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;

namespace Source.Scripts.Ai.MainAis
{
    public class ConstructionsAi
    {
        private readonly UnitsAiRepository _unitsAiRepository;
        private readonly TeamsResourceGlobalStorage _teamsResourceGlobalStorage;
        private readonly List<ConstructionAiBase> _constructionAis = new List<ConstructionAiBase>();
        private readonly Dictionary<Type, ConstructionAiConfigBase> _configs = new Dictionary<Type, ConstructionAiConfigBase>();
        
        public ConstructionsAi(TeamsResourceGlobalStorage teamsResourceGlobalStorage, 
            UnitsAiRepository unitsAiRepository, IEnumerable<ConstructionAiConfigBase> configs)
        {
            _teamsResourceGlobalStorage = teamsResourceGlobalStorage;
            _unitsAiRepository = unitsAiRepository;

            foreach (var config in configs)
                _configs.Add(config.GetType(), config);
        }
        
        public void HandleUpdate(float deltaTime)
        {
            foreach (var constructionAi in _constructionAis)
                constructionAi.HandleUpdate(deltaTime);
        }

        public void TryAddConstruction(ConstructionBase construction)
        {
            if (construction.TryCast(out BeeBarrack beeBarrack))
            {
                var config = _configs[typeof(BeeBarrackAiConfig)];
                var newAi = new BeeBarrackAi(_unitsAiRepository, beeBarrack, config as BeeBarrackAiConfig, _teamsResourceGlobalStorage);
                newAi.ConstructionDestructed += RemoveConstructionAi;
                _constructionAis.Add(newAi);
            }
            
            if (construction.TryCast(out BeeMercenaryBarrack mercenaryBarrack))
            {
                var config = _configs[typeof(BeeMercenaryBarrackAiConfig)];
                var newAi = new BeeMercenaryBarrackAi(_unitsAiRepository, mercenaryBarrack, config as BeeMercenaryBarrackAiConfig, _teamsResourceGlobalStorage);
                newAi.ConstructionDestructed += RemoveConstructionAi;
                _constructionAis.Add(newAi);
            }
            
            if (construction.TryCast(out BeeSiegeWeaponsBarrack beeSiegeWeaponsBarrack))
            {
                var config = _configs[typeof(BeeSiegeWeaponsBarrackAiConfig)];
                var newAi = new BeeSiegeWeaponsBarrackAi(_unitsAiRepository, beeSiegeWeaponsBarrack, config as BeeSiegeWeaponsBarrackAiConfig, _teamsResourceGlobalStorage);
                newAi.ConstructionDestructed += RemoveConstructionAi;
                _constructionAis.Add(newAi);
            }
            
            if (construction.TryCast(out BeeTownHall townHall))
            {
                var config = _configs[typeof(BeeTownHallAiConfig)];
                var newAi = new BeeTownHallAi(_unitsAiRepository, townHall, config as BeeTownHallAiConfig, _teamsResourceGlobalStorage);
                newAi.ConstructionDestructed += RemoveConstructionAi;
                _constructionAis.Add(newAi);
            }
        }

        private void RemoveConstructionAi(ConstructionAiBase constructionAi)
        {
            _constructionAis.Remove(constructionAi);
        }
    }
}