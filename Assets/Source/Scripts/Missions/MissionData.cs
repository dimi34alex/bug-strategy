using System.Collections.Generic;
using BugStrategy.Constructions;
using BugStrategy.NotConstructions;
using BugStrategy.PoisonFog;
using BugStrategy.Projectiles;
using BugStrategy.ResourceSources;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Tiles;
using BugStrategy.Unit;
using BugStrategy.Unit.UnitSelection;
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
        public readonly NotConstructionsGrid NotConstructionsGrid;
        public readonly TilesRepository TilesRepository;
        
        public readonly ITeamsResourcesGlobalStorage TeamsResourcesGlobalStorage;
        public readonly ConstructionSelector ConstructionSelector;
        public readonly UnitsSelector UnitsSelector;

        public FractionType PlayerFraction => FractionTypes[PlayerAffiliation];
        
        public MissionData(
            int missionIndex, 
            MissionConfig missionConfig, 
            IUnitRepository unitRepository, 
            ProjectilesRepository projectilesRepository,
            PoisonFogsRepository poisonFogsRepository,
            ResourceSourcesRepository resourceSourcesRepository,
            ConstructionsRepository constructionsRepository,
            NotConstructionsGrid notConstructionsGrid,
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, 
            UnitsSelector unitsSelector,
            TilesRepository tilesRepository,
            ConstructionSelector constructionSelector) 
        {
            MissionIndex = missionIndex;
            MissionConfig = missionConfig;
            UnitRepository = unitRepository;
            ProjectilesRepository = projectilesRepository;
            PoisonFogsRepository = poisonFogsRepository;
            ResourceSourcesRepository = resourceSourcesRepository;
            ConstructionsRepository = constructionsRepository;
            NotConstructionsGrid = notConstructionsGrid;
            TeamsResourcesGlobalStorage = teamsResourcesGlobalStorage;
            UnitsSelector = unitsSelector;
            TilesRepository = tilesRepository;
            ConstructionSelector = constructionSelector;

            PlayerAffiliation = missionConfig.PlayerAffiliation;
            FractionTypes = missionConfig.FractionByAffiliation;
        }
    }
}