using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.PoisonFog;
using BugStrategy.Projectiles;
using BugStrategy.ResourceSources;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;
using ConstructionsRepository = BugStrategy.Constructions.ConstructionsRepository;

namespace BugStrategy.Missions
{
    public class MissionData
    {
        public readonly int MissionIndex;
        public readonly MissionConfig MissionConfig;

        public readonly AffiliationEnum PlayerAffiliation;
        public readonly IReadOnlyDictionary<AffiliationEnum, FractionType> FractionTypes;

        public readonly IUnitRepository UnitRepository;
        public readonly ProjectilesRepository ProjectilesRepository;
        public readonly PoisonFogsRepository PoisonFogsRepository;
        public readonly ResourceSourcesRepository ResourceSourcesRepository;
        public readonly ConstructionsRepository ConstructionsRepository;
        
        public readonly ITeamsResourcesGlobalStorage TeamsResourcesGlobalStorage;
        public readonly ConstructionSelector ConstructionSelector;

        public FractionType PlayerFraction => FractionTypes[PlayerAffiliation];
        
        public MissionData(
            int missionIndex, 
            MissionConfig missionConfig, 
            IUnitRepository unitRepository, 
            ProjectilesRepository projectilesRepository,
            PoisonFogsRepository poisonFogsRepository,
            ResourceSourcesRepository resourceSourcesRepository,
            ConstructionsRepository constructionsRepository,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
        {
            MissionIndex = missionIndex;
            MissionConfig = missionConfig;
            UnitRepository = unitRepository;
            ProjectilesRepository = projectilesRepository;
            PoisonFogsRepository = poisonFogsRepository;
            ResourceSourcesRepository = resourceSourcesRepository;
            ConstructionsRepository = constructionsRepository;
            TeamsResourcesGlobalStorage = teamsResourcesGlobalStorage;

            PlayerAffiliation = missionConfig.PlayerAffiliation;
            FractionTypes = missionConfig.FractionByAffiliation;
            ConstructionSelector = new ConstructionSelector(ConstructionsRepository);
        }
    }
}