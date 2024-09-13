using Constructions.LevelSystemCore;
using Source.Scripts.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class ButterflyStorage : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private ButterflyStorageConfig config;
       
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyStorage;
        
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
