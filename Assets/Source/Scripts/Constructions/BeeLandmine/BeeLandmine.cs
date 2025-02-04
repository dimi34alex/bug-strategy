using System.Collections.Generic;
using BugStrategy.Constructions.Factory;
using BugStrategy.CustomTimer;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.BeeLandmine
{
    public class BeeLandmine : ConstructionBase, IDamageApplicator
    {
        [SerializeField] private BeeLandmineConfig config;
        [SerializeField] private TriggerBehaviour _triggerBehaviour;
        [SerializeField] private LayerMask layerMask;

        [Inject] private readonly IConstructionFactory _constructionFactory;
        [Inject] private readonly GridConfig _gridConfig;
        
        private Timer _explosionTimer;
        private readonly RaycastHit[] _explosionBuffer = new RaycastHit[32];

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeLandmine;
        protected override ConstructionConfigBase ConfigBase => config;

        public float Damage { get; private set; }
        
        protected override void OnAwake()
        {
            _healthStorage.SetCapacity(config.HealthPoints);
            _healthStorage.SetValue(config.HealthPoints);
            
            Damage = config.Damage;
            _explosionTimer = new Timer(config.ExplosionDelay, 0, true);
            _explosionTimer.OnTimerEnd += Explosion;

            _updateEvent += UpdateExplosionTimer;
        }

        protected override void OnStart()
        {
            _triggerBehaviour.EnterEvent += OnUnitEnter;
        }

        private void UpdateExplosionTimer()
            => _explosionTimer.Tick(Time.deltaTime);
        
        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out UnitBase unit))
            {
                if (Affiliation.CheckEnemies(unit.Affiliation))
                {
                    _triggerBehaviour.EnterEvent -= OnUnitEnter;
                    _explosionTimer.Reset();
                }
            }
        }

        private void Explosion()
        {
            var size = Physics.SphereCastNonAlloc(transform.position, config.ExplosionRadius, Vector3.down,
                _explosionBuffer, 0, layerMask);

            for (int i = 0; i < size; i++)
            {
                if (_explosionBuffer[i].collider.gameObject.TryGetComponent(out IDamagable damageable) 
                    && damageable.IsAlive
                    && Affiliation.CheckEnemies(damageable.Affiliation));
                {
                    damageable.TakeDamage(this, this);
                }
            }

            MissionData.ConstructionsRepository.GetConstruction(transform.position, true);
            SendDeactivateEvent();
            SpawnStickyTile();
            Destroy(gameObject);
        }

        private void SpawnStickyTile()
        {
            var radius = config.StickyTilesRadius;
            if(radius <= 0)
                return;

            var prevSpawnedTiles = new List<Vector3>();

            SpawnStickyTile(transform.position);
            prevSpawnedTiles.Add(transform.position);
            
            for (int i = 1; i < radius; i++)
            {
                var newSpawnedTiles = new List<Vector3>();
                for (int j = 0; j < prevSpawnedTiles.Count; j++)
                {
                    var tilePos = prevSpawnedTiles[j];
                    Vector3 hexOffsets = new Vector3(_gridConfig.HexagonsOffsets.x / 2, _gridConfig.HexagonsOffsets.y);

                    var posY = tilePos + new Vector3(0, 0, hexOffsets.y * 2);
                    var posNegativeY = tilePos + new Vector3(0, 0, -hexOffsets.y * 2);
                    var posXZ = tilePos + new Vector3(hexOffsets.x, 0, hexOffsets.y);
                    var posNegativeXZ = tilePos + new Vector3(-hexOffsets.x, 0, hexOffsets.y);
                    var posXNegativeZ = tilePos + new Vector3(hexOffsets.x, 0, -hexOffsets.y);
                    var posNegativeXNegativeZ = tilePos + new Vector3(-hexOffsets.x, 0, -hexOffsets.y);
                    
                    SpawnStickyTile(posY);
                    newSpawnedTiles.Add(posY);
                    
                    SpawnStickyTile(posNegativeY);
                    newSpawnedTiles.Add(posNegativeY);
                    
                    SpawnStickyTile(posXZ);
                    newSpawnedTiles.Add(posXZ);
                    
                    SpawnStickyTile(posNegativeXZ);
                    newSpawnedTiles.Add(posNegativeXZ);
                    
                    SpawnStickyTile(posXNegativeZ);
                    newSpawnedTiles.Add(posXNegativeZ);
                    
                    SpawnStickyTile(posNegativeXNegativeZ);
                    newSpawnedTiles.Add(posNegativeXNegativeZ);
                }

                prevSpawnedTiles = newSpawnedTiles;
            }
        }

        private void SpawnStickyTile(Vector3 position)
        {
            if(MissionData.ConstructionsRepository.ConstructionExist(position))
                return;
            
            _constructionFactory.Create<BeeStickyTile.BeeStickyTile>(ConstructionID.BeeStickyTileConstruction, position, Affiliation);
        }
    }
}