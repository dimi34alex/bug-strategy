using Constructions.LevelSystemCore;
using UnityEngine;

namespace Constructions
{
    public class AntStoreHouse : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private AntStoreHouseConfig config;

        public override ConstructionID ConstructionID => ConstructionID.AntStoreHouse;
        public IConstructionLevelSystem LevelSystem { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new AntStoreHouseLevelSystem(config.Levels, ref resourceRepository, ref _healthStorage);
        }
    }
}