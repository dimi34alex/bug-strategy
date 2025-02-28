using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntHouse
{
    public class AntHouse : StorageBase
    {
        [SerializeField] private AntHouseConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntHouse;
        protected override ConstructionConfigBase ConfigBase => config;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntHouseLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}