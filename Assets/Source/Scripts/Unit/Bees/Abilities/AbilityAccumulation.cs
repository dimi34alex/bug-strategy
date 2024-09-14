using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.CustomTimer;
using BugStrategy.Missions;
using BugStrategy.Unit.AbilitiesCore;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    public sealed class AbilityAccumulation : IDamageApplicator, IPassiveAbility
    {
        private readonly Bumblebee _bumblebee;
        private readonly float _explosionRadius;
        private readonly float _explosionDamage;
        private readonly LayerMask _explosionLayers;
        private readonly IConstructionFactory _constructionFactory;
        private readonly MissionData _missionData;

        public IReadOnlyTimer Cooldown { get; } = new Timer(1, 1);
        public float Damage => _explosionDamage;
        public AbilityType AbilityType => AbilityType.Accumulation;
        public AffiliationEnum Affiliation => _bumblebee.Affiliation;
        
        public AbilityAccumulation(Bumblebee bumblebee, float explosionRadius, float explosionDamage, 
            LayerMask explosionLayers, IConstructionFactory constructionFactory, MissionData missionData)
        {
            _bumblebee = bumblebee;
            _explosionRadius = explosionRadius;
            _explosionDamage = explosionDamage;
            _explosionLayers = explosionLayers;
            _constructionFactory = constructionFactory;
            _missionData = missionData;

            _bumblebee.OnUnitDiedEvent += Explosion;
        }

        private void Explosion()
        {
            DamageNearEnemies();
            TrySpawnStickyTile();
        }

        private void DamageNearEnemies()
        {
            RaycastHit[] result = new RaycastHit[100];
            var size = Physics.SphereCastNonAlloc(_bumblebee.transform.position, _explosionRadius, Vector3.down,
                result, 0, _explosionLayers);

            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) 
                    && Affiliation.CheckEnemies(damageable.Affiliation))
                {
                    damageable.TakeDamage(_bumblebee, this);
                }
            }
        }

        private void TrySpawnStickyTile()
        {
            var roundedPosition = 
                _missionData.ConstructionsRepository
                    .RoundPositionToGrid(_bumblebee.transform.position);
            
            if(_missionData.ConstructionsRepository.ConstructionExist(roundedPosition))
                return;
            
            var construction = _constructionFactory.Create<ConstructionBase>(ConstructionID.BeeStickyTileConstruction, Affiliation);
            construction.transform.position = roundedPosition;
            _missionData.ConstructionsRepository.AddConstruction(roundedPosition, construction);
        }
    }
}