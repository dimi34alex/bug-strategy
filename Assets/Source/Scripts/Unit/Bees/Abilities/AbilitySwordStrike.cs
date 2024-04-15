using CustomTimer;
using Unit.AbilitiesCore;
using UnityEngine;

namespace Unit.Bees
{
    public sealed class AbilitySwordStrike : IDamageApplicator, IAbility
    {
        private readonly UnitBase _unitBase;
        private readonly float _distanceFromCenter;
        private readonly float _attackRadius;
        private readonly Timer _cooldown;
        private readonly LayerMask _attackLayers;
        
        public float Damage { get; }
        public IReadOnlyTimer Cooldown => _cooldown;
        public AbilityType AbilityType => AbilityType.SwordStrike;
        
        public AbilitySwordStrike(UnitBase unitBase, HorntailAttackProcessor attackProcessor, float attackDamage, float distanceFromCenter, float attackRadius, float cooldown, LayerMask attackLayers)
        {
            _unitBase = unitBase;
            _distanceFromCenter = distanceFromCenter;
            Damage = attackDamage;
            _attackRadius = attackRadius;
            _cooldown = new Timer(cooldown);
            _attackLayers = attackLayers; 
            
            attackProcessor.TargetAttacked += TryActivateAbility;
        }

        public void HandleUpdate(float time)
        {
            _cooldown.Tick(time);
        }

        public void Reset()
        {
            _cooldown.Reset();
        }
        
        private void TryActivateAbility(IUnitTarget target)
        {
            if (!_cooldown.TimerIsEnd) 
                return;
            
            ActivateAbility(target);
            Reset();
        }

        private void ActivateAbility(IUnitTarget target)
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

        public void LoadData(float currentCooldownValue)
        {
            Reset();
            _cooldown.SetCurrentTIme(currentCooldownValue);
        }
    }
}