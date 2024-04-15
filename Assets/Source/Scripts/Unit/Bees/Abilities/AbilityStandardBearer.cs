using AttackCooldownChangerSystem;
using MoveSpeedChangerSystem;
using Unit.AbilitiesCore;

namespace Unit.Bees
{
    public sealed class AbilityStandardBearer : IAbility
    {
        private readonly SphereTrigger _abilityZone;
        private readonly float _attackSpeedIncreasePower;
        private readonly float _moveSpeedIncreasePower;
        
        public AbilityType AbilityType => AbilityType.StandardBearer;
        
        public AbilityStandardBearer(SphereTrigger abilityZone, float attackSpeedIncreasePower, float moveSpeedIncreasePower, 
            float abilityRadius)
        {
            _abilityZone = abilityZone;
            _attackSpeedIncreasePower = attackSpeedIncreasePower;
            _moveSpeedIncreasePower = moveSpeedIncreasePower;
            _abilityZone.SetRadius(abilityRadius);
            
            _abilityZone.EnterEvent += OnEnterInAbilityZone;
            _abilityZone.ExitEvent += OnExitFromAbilityZone;
        }

        private void OnEnterInAbilityZone(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IAttackCooldownChangeable attackSpeedChangeable) 
                && attackSpeedChangeable.Affiliation == AffiliationEnum.Bees)
            {
                attackSpeedChangeable.AttackCooldownChanger.Apply(_attackSpeedIncreasePower);
            }
            
            if (triggerable.TryCast(out IMoveSpeedChangeable moveSpeedChangeable) 
                && moveSpeedChangeable.Affiliation == AffiliationEnum.Bees)
            {
                moveSpeedChangeable.MoveSpeedChangerProcessor.Apply(_moveSpeedIncreasePower);
            }
        }

        private void OnExitFromAbilityZone(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IAttackCooldownChangeable attackSpeedChangeable) 
                && attackSpeedChangeable.Affiliation == AffiliationEnum.Bees)
            {
                attackSpeedChangeable.AttackCooldownChanger.DeApply(_attackSpeedIncreasePower);
            }
            
            if (triggerable.TryCast(out IMoveSpeedChangeable moveSpeedChangeable) 
                && moveSpeedChangeable.Affiliation == AffiliationEnum.Bees)
            {
                moveSpeedChangeable.MoveSpeedChangerProcessor.DeApply(_moveSpeedIncreasePower);
            }
        }
    }
}