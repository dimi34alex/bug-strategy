using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ButterflyHouse
{
    public class ButterflyHouse : StorageBase
    {
        [SerializeField] private ButterflyHouseConfig config;
     
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyHouse;
        protected override ConstructionConfigBase ConfigBase => config;

        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new ButterflyHouseLevelSystem(this, config, _teamsResourcesGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}