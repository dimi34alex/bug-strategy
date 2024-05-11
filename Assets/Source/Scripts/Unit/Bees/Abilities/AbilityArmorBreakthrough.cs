using System.Collections.Generic;
using Unit.AbilitiesCore;
using Unit.Factory;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class AbilityArmorBreakthrough : IDamageApplicator, IAbility
    {
        private readonly IReadOnlyDictionary<UnitType, int> _spawnUnits;
        private readonly UnitFactory _unitFactory;
        private readonly MobileHive _mobileHive;
        private readonly float _explosionRadius;
        private readonly LayerMask _explosionLayers;
        
        public float Damage { get; private set; }
        public AbilityType AbilityType => AbilityType.ArmorBreakthrough;

        public AbilityArmorBreakthrough(MobileHive mobileHive, float explosionDamage, float explosionRadius, LayerMask explosionLayers,  UnitFactory unitFactory, IReadOnlyDictionary<UnitType, int> spawnUnits)
        {
            _mobileHive = mobileHive;
            Damage = explosionDamage;
            _explosionRadius = explosionRadius;
            _explosionLayers = explosionLayers;
            _spawnUnits = spawnUnits;
            _unitFactory = unitFactory;
            
            _mobileHive.OnUnitDied += ActivateAbility;
        }

        private void ActivateAbility(UnitBase unitBase)
        {
            Explosion();
            SpawnUnits();
        }
        
        private void Explosion()
        {
            RaycastHit[] result = new RaycastHit[50];
            var size = Physics.SphereCastNonAlloc(_mobileHive.transform.position, _explosionRadius, Vector3.down,
                result, 0, _explosionLayers);

            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable)
                    && _mobileHive.Affiliation.CheckEnemies(damageable.Affiliation))
                {
                    damageable.TakeDamage(_mobileHive, this);
                }
            }  
        }
        
        private void SpawnUnits()
        {
            foreach (var spawnUnit in _spawnUnits)
            {
                for (int i = 0; i < spawnUnit.Value; i++)
                {
                    float randomPos = Random.value; 
                    var spawnPosition = _mobileHive.transform.position + Vector3.left * randomPos;
                    _unitFactory.Create(spawnUnit.Key, spawnPosition, _mobileHive.Affiliation);
                }
            }
        }
    }
}