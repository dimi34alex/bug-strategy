using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.AntRangeWorkshop
{
    public class AntRangeWorkshop : AntWorkshopBase
    {
        [SerializeField] private AntRangeWorkshopConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;

        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntRangeWorkshop;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntRangeWorkshopLevelSystem(this, config, _teamsResourcesGlobalStorage, _healthStorage);
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
    }
}