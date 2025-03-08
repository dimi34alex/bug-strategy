using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.Constructions.Factory;
using BugStrategy.NotConstructions;
using BugStrategy.NotConstructions.Factory;
using BugStrategy.Projectiles.Factory;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.BeeWaxTower
{
    public class BeeWaxTower : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private TriggerBehaviour attackZone;
        [SerializeField] private BeeWaxTowerConfig config;

        [Inject] private readonly ProjectilesFactory _projectilesFactory;
        [Inject] private readonly INotConstructionFactory _notConstructionFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeWaxTower;
        protected override ConstructionConfigBase ConfigBase => config;

        public IConstructionLevelSystem LevelSystem => _levelSystem;
        private BeeWaxTowerLevelSystem _levelSystem;
        
        private BeeWaxTowerAttackProcessor _attackProcessor;

        protected override void OnAwake()
        {
            base.OnAwake();

            _attackProcessor = new BeeWaxTowerAttackProcessor(this, _projectilesFactory, attackZone, transform, this);

            _levelSystem = new BeeWaxTowerLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage,
                _healthStorage, _attackProcessor);
            
            _updateEvent += UpdateAttackProcessor;
            OnDestruction += SpawnStickyTile;

            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
        {
            _levelSystem.Init(0);
        }
        
        private void UpdateAttackProcessor()
            => _attackProcessor.HandleUpdate(Time.deltaTime);

        private void SpawnStickyTile() 
            => 
        _notConstructionFactory.Create<NotConstructionBase>(NotConstructionID.BeeStickyTileConstruction, transform.position, Affiliation);
    }
}