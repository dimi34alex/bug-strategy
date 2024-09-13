using System;
using System.Collections.Generic;
using Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators.Configs;
using Source.Scripts.Ai.MainAis;
using Source.Scripts.Missions;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;
using Unit.Factory;

namespace Source.Scripts.Ai
{
    public class AisProvider
    {
        private readonly List<IGlobalAi> _globalAis;
        
        public AisProvider(MissionData missionData, UnitFactory unitFactory, IConstructionFactory constructionFactory,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, float timeBeforeAttackPlayerTownHall,
            IReadOnlyCollection<ConstructionAiConfigBase> constructionAiConfigs)
        {
            _globalAis = new List<IGlobalAi>(missionData.FractionTypes.Count - 1);
            
            foreach (var fractionByAffiliation in missionData.FractionTypes)
            {
                if (missionData.PlayerAffiliation != fractionByAffiliation.Key)
                {
                    switch (fractionByAffiliation.Value)
                    {
                        case FractionType.None:
                            break;
                        case FractionType.Bees:
                            _globalAis.Add(new BeesGlobalAi(fractionByAffiliation.Key, unitFactory, 
                                constructionFactory, timeBeforeAttackPlayerTownHall, 
                                teamsResourcesGlobalStorage, constructionAiConfigs));
                            break;
                        case FractionType.Ants:
                            break;
                        case FractionType.Butterflies:
                            break;
                        case FractionType.Neutral:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public void ManualUpdate(float deltaTime)
        {
            foreach (var globalAi in _globalAis) 
                globalAi.HandleUpdate(deltaTime);
        } 
    }
}