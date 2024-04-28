using Constructions.LevelSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class BeeStorage : StorageBase
    {
        [SerializeField] private BeeStorageConfig config;
       
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeStorage;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(0, 0);
            LevelSystem = new BeeStorageLevelSystem(this, config, _resourceGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}