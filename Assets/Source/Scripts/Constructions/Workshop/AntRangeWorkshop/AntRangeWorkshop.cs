using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Unit.Ants;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntRangeWorkshop
{
    public class AntRangeWorkshop : AntWorkshopBase
    {
        [SerializeField] private AntRangeWorkshopConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntRangeWorkshop;
        protected override ConstructionConfigBase ConfigBase => config;

        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
        public override IConstructionLevelSystem LevelSystem { get; protected set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntRangeWorkshopLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage, _healthStorage, WorkshopCore);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}