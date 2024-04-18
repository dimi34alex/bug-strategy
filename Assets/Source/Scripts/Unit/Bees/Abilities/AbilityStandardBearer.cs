using System.Collections.Generic;
using Unit.AbilitiesCore;
using Unit.Effects;

namespace Unit.Bees
{
    public sealed class AbilityStandardBearer : IAbility
    {
        private readonly SphereTrigger _abilityZone;
        private readonly List<IEffectable> _enters = new List<IEffectable>();
        
        public AbilityType AbilityType => AbilityType.StandardBearer;
        
        public AbilityStandardBearer(SphereTrigger abilityZone, float abilityRadius)
        {
            _abilityZone = abilityZone;
            _abilityZone.SetRadius(abilityRadius);
            
            _abilityZone.EnterEvent += OnEnterInAbilityZone;
            _abilityZone.ExitEvent += OnExitFromAbilityZone;
        }

        private void OnEnterInAbilityZone(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IEffectable effectable)
                && effectable.Affiliation == AffiliationEnum.Bees 
                && !_enters.Contains(effectable))
            {
                _enters.Add(effectable);
                effectable.EffectsProcessor.ApplyEffect(EffectType.AttackCooldownDecrease, true);
                effectable.EffectsProcessor.ApplyEffect(EffectType.MoveSpeedUp, true);
            }
        }

        private void OnExitFromAbilityZone(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IEffectable effectable)
                && _enters.Contains(effectable))
            {
                _enters.Remove(effectable);
                effectable.EffectsProcessor.RemoveFixedEnter(EffectType.AttackCooldownDecrease);
                effectable.EffectsProcessor.RemoveFixedEnter(EffectType.MoveSpeedUp);
            }
        }
    }
}