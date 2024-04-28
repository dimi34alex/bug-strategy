using Constructions.LevelSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class AntWorkerWorkshop : AntWorkshopBase
    {
        [SerializeField] private AntWorkerWorkshopConfig config;

        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntWorkerWorkshop;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntWorkerWorkshopLevelSystem(this, config, _resourceGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}