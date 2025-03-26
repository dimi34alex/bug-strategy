using BugStrategy.Constructions;
using BugStrategy.Effects;
using BugStrategy.Missions;
using BugStrategy.NotConstructions;
using BugStrategy.NotConstructions.Factory;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Projectiles
{
    public class HoneyCatapultProjectile : ProjectileBase
    {
        [SerializeField] private LayerMask layerMask;

        [Inject] private readonly StickConfig _stickConfig;
        [Inject] private readonly INotConstructionFactory _notConstructionFactory;
        [Inject] private readonly MissionData _missionData;

        private IConstructionGrid ConstructionGrid => _missionData.ConstructionsRepository;
        private NotConstructionsGrid NotConstructionsGrid => _missionData.NotConstructionsGrid;
        
        public override ProjectileType ProjectileType => ProjectileType.HoneyCatapultProjectile;

        private float _constructionDamageScale;
        private float _damageRadius;
        private bool _isSticky;

        public void SetSticky()
            => _isSticky = true;

        public void SetDamageRadius(float damageRadius)
            => _damageRadius = damageRadius;
        
        public void SetConstructionDamageScale(float constructionDamageScale)
            => _constructionDamageScale = constructionDamageScale;
        
        public override void OnElementReturn()
        {
            base.OnElementReturn();
            _isSticky = false;
        }

        protected override void CollideWithTarget(ITarget target)
        {
            RaycastHit[] result = new RaycastHit[50];
            var size = Physics.SphereCastNonAlloc(transform.position, _damageRadius, Vector3.down,
                result, 0, layerMask);

            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) 
                    && damageable.IsAlive
                    && Affiliation.CheckEnemies(damageable.Affiliation))
                {
                    float damageScale = 1;

                    if (target.TryCast(out IEffectable effectable) && 
                        effectable.EffectsProcessor.HaveEffect(EffectType.StickyHoney))
                        damageScale *= _stickConfig.TakeDamageScale;
                    
                    if (target.CastPossible<IConstruction>())
                        damageScale *= _constructionDamageScale;
                    
                    damageable.TakeDamage(Attacker, this, damageScale);
                }
            }  
            
            if (_isSticky)
                TrySpawnStickyTile();
            
            ReturnInPool();
        }
        
        private void TrySpawnStickyTile()
        {
            var roundedPosition = ConstructionGrid.RoundPositionToGrid(transform.position);
            if(!ConstructionGrid.IsFree(roundedPosition))
                return;
            if(!NotConstructionsGrid.IsFree(roundedPosition))
                return;
            
            _notConstructionFactory.Create<NotConstructionBase>(NotConstructionID.BeeStickyTileConstruction, roundedPosition, Affiliation);
        }
    }
}