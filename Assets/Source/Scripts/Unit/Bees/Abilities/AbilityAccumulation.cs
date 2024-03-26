using UnityEngine;

namespace Unit.Bees
{
    public class AbilityAccumulation : IDamageApplicator
    {
        private readonly Bumblebee _bumblebee;
        private readonly float _explosionRadius;
        private readonly float _explosionDamage;
        private readonly LayerMask _explosionLayers;
        private readonly IConstructionFactory _constructionFactory;

        public float Damage => _explosionDamage;
        
        public AbilityAccumulation(Bumblebee bumblebee, float explosionRadius, float explosionDamage, LayerMask explosionLayers, IConstructionFactory constructionFactory)
        {
            _bumblebee = bumblebee;
            _explosionRadius = explosionRadius;
            _explosionDamage = explosionDamage;
            _explosionLayers = explosionLayers;
            _constructionFactory = constructionFactory;
            
            _bumblebee.OnDeactivation += Explosion;
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
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) &&
                    damageable.Affiliation != AffiliationEnum.Bees)
                {
                    damageable.TakeDamage(this);
                }
            }
        }

        private void TrySpawnStickyTile()
        {
            if(FrameworkCommander.GlobalData.ConstructionsRepository.ConstructionExist(_bumblebee.transform.position))
                return;
            
            ConstructionBase construction = _constructionFactory.Create<ConstructionBase>(ConstructionID.BeeStickyTileConstruction);
            FrameworkCommander.GlobalData.ConstructionsRepository.AddConstruction(_bumblebee.transform.position, construction);
            construction.transform.position = _bumblebee.transform.position;
        }
    }
}