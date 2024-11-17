using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.Ants;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntMeleeWorkshop
{
    public class AntMeleeWorkshop : AntWorkshopBase
    {
        [SerializeField] private AntMeleeWorkshopConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntMeleeWorkshop;

        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;
        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntMeleeWorkshopLevelSystem(this, config, _teamsResourcesGlobalStorage, _healthStorage, WorkshopCore);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}