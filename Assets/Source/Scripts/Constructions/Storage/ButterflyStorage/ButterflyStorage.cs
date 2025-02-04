using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ButterflyStorage
{
    public class ButterflyStorage : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private ButterflyStorageConfig config;
       
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyStorage;
        protected override ConstructionConfigBase ConfigBase => config;

        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new ButterflyStorageLevelSystem(this, config, _teamsResourcesGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }   
}
