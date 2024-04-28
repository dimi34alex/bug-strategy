using Constructions.LevelSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class ButterflyHouse : StorageBase
    {
        [SerializeField] private ButterflyHouseConfig config;
     
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyHouse;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new ButterflyHouseLevelSystem(this, config, _resourceGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}