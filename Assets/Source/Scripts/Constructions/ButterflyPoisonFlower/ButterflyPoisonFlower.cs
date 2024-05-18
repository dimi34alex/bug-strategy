using Constructions.LevelSystemCore;
using PoisonFog.Factory;
using Projectiles.Factory;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class ButterflyPoisonFlower : ConstructionBase, IEvolveConstruction
    {
        [SerializeField] private ButterflyPoisonFlowerConfig config;
        [SerializeField] private TriggerBehaviour triggerBehaviour;
        [SerializeField] private SphereCollider attackZoneCollider;

        [Inject] private readonly ProjectileFactory _projectileFactory;
        [Inject] private readonly PoisonFogFactory _poisonFogFactory;
        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;

        private ButterflyPoisonFlowerAttackProcessor _attackProcessor;
        private ButterflyPoisonFlowerPoisonFogProcessor _poisonFogProcessor;

        public override FractionType Fraction => FractionType.Butterflies;
        public override ConstructionID ConstructionID => ConstructionID.ButterflyPoisonFlower;
        
        public IConstructionLevelSystem LevelSystem { get; private set; }
        
        protected override void OnAwake()
        {
            _attackProcessor = new ButterflyPoisonFlowerAttackProcessor(this, transform, _projectileFactory, triggerBehaviour);
            _poisonFogProcessor = new ButterflyPoisonFlowerPoisonFogProcessor(transform, _poisonFogFactory);
            
            LevelSystem = new ButterflyPoisonFlowerLevelSystem(this, config, _resourceGlobalStorage, _healthStorage, 
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