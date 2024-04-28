using Constructions.LevelSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class AntFort : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private AntFortConfig config;

        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntFort;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntFortLevelSystem(this, config, _resourceGlobalStorage,  _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}