using System.Collections.Generic;
using Unit.AbilitiesCore;
using Unit.Effects;

namespace Unit.Bees
{
    public sealed class AbilityStandardBearer : IAbility
    {
        private readonly IAffiliation _affiliation;
        private readonly SphereTrigger _abilityZone;
        private readonly List<IEffectable> _enters = new List<IEffectable>();

        public AffiliationEnum Affiliation => _affiliation.Affiliation;
        public AbilityType AbilityType => AbilityType.StandardBearer;
        
        public AbilityStandardBearer(IAffiliation affiliation, SphereTrigger abilityZone, float abilityRadius)
        {
            _affiliation = affiliation;
            _abilityZone = abilityZone;
            _abilityZone.SetRadius(abilityRadius);
            
            _abilityZone.EnterEvent += OnEnterInAbilityZone;
            _abilityZone.ExitEvent += OnExitFromAbilityZone;
        }

        private void OnEnterInAbilityZone(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IEffectable effectable)
                && Affiliation.CheckEnemies(effectable.Affiliation) 
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