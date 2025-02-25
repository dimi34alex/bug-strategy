using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.PoisonFog.Factory;
using BugStrategy.Projectiles.Factory;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Trigger;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ButterflyPoisonFlower
{
    public class ButterflyPoisonFlower : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private ButterflyPoisonFlowerConfig config;
        [SerializeField] private TriggerBehaviour triggerBehaviour;
        [SerializeField] private SphereCollider attackZoneCollider;

        [Inject] private readonly ProjectilesFactory _projectilesFactory;
        [Inject] private readonly PoisonFogFactory _poisonFogFactory;
        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        [Inject] private readonly TechnologyModule _technologyModule;

        private ButterflyPoisonFlowerAttackProcessor _attackProcessor;
        private ButterflyPoisonFlowerPoisonFogProcessor _poisonFogProcessor;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyPoisonFlower;
        protected override ConstructionConfigBase ConfigBase => config;

        public IConstructionLevelSystem LevelSystem { get; private set; }
        
        protected override void OnAwake()
        {
            _attackProcessor = new ButterflyPoisonFlowerAttackProcessor(this, transform, _projectilesFactory,
                triggerBehaviour, this);
            _poisonFogProcessor = new ButterflyPoisonFlowerPoisonFogProcessor(transform, _poisonFogFactory);
            
            LevelSystem = new ButterflyPoisonFlowerLevelSystem(this, _technologyModule, config, _teamsResourcesGlobalStorage, _healthStorage, 
                ref attackZoneCollider, ref _attackProcessor, ref _poisonFogProcessor);
            
            OnDestruction += _attackProcessor.KillCooldownTimer;
            OnDestruction += _poisonFogProcessor.SpawnPoisonFog;

            _updateEvent += UpdateAttackProcessor;
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
        private void UpdateAttackProcessor()
            => _attackProcessor.HandleUpdate(Time.deltaTime);

        //TODO: remove this temporary code, when ui will be create
        [ContextMenu("LevelUp")]
        private void LevelUp() 
            => LevelSystem.TryLevelUp();
    }
}