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
        public readonly ConstructionSelector ConstructionSelector;

        public FractionType PlayerFraction => FractionTypes[PlayerAffiliation];
        
        private MissionData()
        {
            ConstructionSelector = new ConstructionSelector(ConstructionsRepository);
        }
        
        public MissionData(int missionIndex, MissionConfig missionConfig) 
            : this()
        {
            MissionIndex = missionIndex;
            PlayerAffiliation = missionConfig.PlayerAffiliation;
            FractionTypes = missionConfig.FractionByAffiliation;
        }
    }
}