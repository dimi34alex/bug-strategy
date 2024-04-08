using CustomTimer;
using UnityEngine;

namespace Unit.Bees
{
    public class AbilitySwordStrike : IDamageApplicator
    {
        private readonly UnitBase _unitBase;
        private readonly HorntailAttackProcessor _attackProcessor;
        private readonly float _distanceFromCenter;
        private readonly float _attackRadius;
        private readonly Timer _cooldown;
        private readonly LayerMask _attackLayers;
        
        public float Damage { get; }

        public AbilitySwordStrike(UnitBase unitBase, HorntailAttackProcessor attackProcessor, float attackDamage, float distanceFromCenter, float attackRadius, float cooldown, LayerMask attackLayers)
        {
            _unitBase = unitBase;
            _distanceFromCenter = distanceFromCenter;
            _attackProcessor = attackProcessor;
            Damage = attackDamage;
            _attackRadius = attackRadius;
            _cooldown = new Timer(cooldown, cooldown);
            _attackLayers = attackLayers; 
            
            attackProcessor.OnAttackTarget += TryActivate;
        }

        public void HandleUpdate(float time)
        {
            _cooldown.Tick(time);
        }
        
        private void TryActivate(IUnitTarget target)
        {
            if (!_cooldown.TimerIsEnd) 
                return;
            
            SwordStrike(target);
            _cooldown.Reset();
        }

        private void SwordStrike(IUnitTarget target)
        {
            var swordStrikeDirection = (_unitBase.Transform.position - target.Transform.position).normalized;
            
            RaycastHit[] result = new RaycastHit[50];
            var size = Physics.SphereCastNonAlloc(
                _unitBase.Transform.position + swordStrikeDirection * _distanceFromCenter,
                _attackRadius, Vector3.down, result, 0, _attackLayers);
            
            for (int i = 0; i < size; i++)
            {
                if (result[i].collider.gameObject.TryGetComponent(out IDamagable damageable) && 
                    damageable.Affiliation != AffiliationEnum.Bees)
                {
                    damageable.TakeDamage(this);
                }
            }
        }
    }
}