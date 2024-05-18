using Unit.AbilitiesCore;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class AbilityAccumulation : IDamageApplicator, IAbility
    {
        private readonly Bumblebee _bumblebee;
        private readonly float _explosionRadius;
        private readonly float _explosionDamage;
        private readonly LayerMask _explosionLayers;
        private readonly IConstructionFactory _constructionFactory;
        
        public float Damage => _explosionDamage;
        public AbilityType AbilityType => AbilityType.Accumulation;
        public AffiliationEnum Affiliation => _bumblebee.Affiliation;
        
        public AbilityAccumulation(Bumblebee bumblebee, float explosionRadius, float explosionDamage, LayerMask explosionLayers, IConstructionFactory constructionFactory)
        {
            _bumblebee = bumblebee;
            _explosionRadius = explosionRadius;
            _explosionDamage = explosionDamage;
            _explosionLayers = explosionLayers;
            _constructionFactory = constructionFactory;
            
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
                    damageable.TakeDamage(this);
                }
            }
        }

        private void TrySpawnStickyTile()
        {
            var roundedPosition = 
                FrameworkCommander.GlobalData.ConstructionsRepository
                    .RoundPositionToGrid(_bumblebee.transform.position);
            
            if(FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(roundedPosition))
                return;
            
            ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(ConstructionID.BeeStickyTileConstruction, Affiliation);
            construction.transform.position = roundedPosition;
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(roundedPosition, construction);
        }
    }
}