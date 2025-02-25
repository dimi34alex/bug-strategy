using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntFort
{
    public class AntFort : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private AntFortConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;
        
        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntFort;
        protected override ConstructionConfigBase ConfigBase => config;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntFortLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage,  _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}