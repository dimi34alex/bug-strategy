using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Ants;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntWorkerWorkshop
{
    public class AntWorkerWorkshop : AntWorkshopBase
    {
        [SerializeField] private AntWorkerWorkshopConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntWorkerWorkshop;
        protected override ConstructionConfigBase ConfigBase => config;

        public override ProfessionType ProfessionType => ProfessionType.Worker;
        public override IConstructionLevelSystem LevelSystem { get; protected set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntWorkerWorkshopLevelSystem(this, config, _teamsResourcesGlobalStorage, _healthStorage, WorkshopCore);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}