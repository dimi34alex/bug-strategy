using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BugStrategy.Effects
{
    public class EffectsFactory : MonoBehaviour
    {
        [Inject] private readonly PoisonConfig _poisonConfig;
        [Inject] private readonly StickyHoneyConfig _stickyHoneyConfig;
        [Inject] private readonly MoveSpeedUpConfig _moveSpeedUpConfig;
        [Inject] private readonly MoveSpeedDownConfig _moveSpeedDownConfig;
        [Inject] private readonly AttackCooldownDecreaseConfig _attackCooldownDecreaseConfig;
        
        private Dictionary<EffectType, EffectCreatorBase> _effectCreators;

        private void Awake()
        {
            _effectCreators = new Dictionary<EffectType, EffectCreatorBase>()
            {
                { EffectType.Poison, new PoisonCreator(_poisonConfig) },
                { EffectType.StickyHoney, new StickyHoneyCreator(_stickyHoneyConfig) },
                { EffectType.MoveSpeedUp, new MoveSpeedUpCreator(_moveSpeedUpConfig) },
                { EffectType.MoveSpeedDown, new MoveSpeedDownCreator(_moveSpeedDownConfig) },
                { EffectType.AttackCooldownDecrease, new AttackCooldownDecreaseCreator(_attackCooldownDecreaseConfig) }
            };
        }

        public bool CreateEffect(EffectType effectType, IEffectable effectable, out EffectProcessorBase effectProcessor)
            => _effectCreators[effectType].Create(effectable, out effectProcessor);
    }
}