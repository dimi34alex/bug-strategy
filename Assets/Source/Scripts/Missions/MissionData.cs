using System.Collections.Generic;
using PoisonFog;
using Projectiles;

namespace Source.Scripts.Missions
{
    public class MissionData
    {
        public readonly int MissionIndex;
        
        public readonly AffiliationEnum PlayerAffiliation;
        public readonly IReadOnlyDictionary<AffiliationEnum, FractionType> FractionTypes;

        public readonly UnitRepository UnitRepository = new();
        public readonly ResourceRepository ResourceRepository = new();
        public readonly ProjectilesRepository ProjectilesRepository = new();
        public readonly PoisonFogsRepository PoisonFogsRepository = new();
        public readonly ResourceSourcesRepository ResourceSourcesRepository = new();
        public readonly ConstructionsRepository ConstructionsRepository = new();
        
        public readonly TeamsResourceGlobalStorage TeamsResourceGlobalStorage;
        public readonly ConstructionSelector ConstructionSelector;

        public FractionType PlayerFraction => FractionTypes[PlayerAffiliation];
        
        public MissionData(int missionIndex, MissionConfig missionConfig, TeamsResourceGlobalStorage teamsResourceGlobalStorage) 
        {
            MissionIndex = missionIndex;
            PlayerAffiliation = missionConfig.PlayerAffiliation;
            FractionTypes = missionConfig.FractionByAffiliation;
            TeamsResourceGlobalStorage = teamsResourceGlobalStorage;
            ConstructionSelector = new ConstructionSelector(ConstructionsRepository);
        }
    }
}