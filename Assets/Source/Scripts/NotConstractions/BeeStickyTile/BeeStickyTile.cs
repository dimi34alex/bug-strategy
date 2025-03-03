using System.Collections.Generic;
using BugStrategy.CustomTimer;
using BugStrategy.Effects;
using BugStrategy.Trigger;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.NotConstructions.BeeStickyTile
{
    public class BeeStickyTile : NotConstructionBase
    {
        [SerializeField] private BeeStickyTileConfig config;
        [SerializeField] private TriggerBehaviour stickZone;
        
        private readonly List<BufferBeforeApplyStick> _buffers = new();
        private Timer _existsTimer;

        public override FractionType Fraction => FractionType.Bees;
        public override NotConstructionID NotConstructionID => NotConstructionID.BeeStickyTileConstruction;
        protected override NotConstructionConfigBase ConfigBase => config;

        protected override void OnAwake()
        {
            _healthStorage.SetCapacity(config.HealthPoints);
            _healthStorage.SetValue(config.HealthPoints);

            _existsTimer = new Timer(config.ExistTime);
            _existsTimer.OnTimerEnd += DestructStickyTile;
            
            _updateEvent += UpdateExistsTimer;
            _updateEvent += UpdateTimers;
        }

        protected override void OnStart()
        {
            stickZone.EnterEvent += OnUnitEnter;
            stickZone.ExitEvent += OnUnitExit;
        }

        private void UpdateExistsTimer()
            => _existsTimer.Tick(Time.deltaTime);
        
        private void UpdateTimers()
        {
            var time = Time.deltaTime;
            foreach (var buffer in _buffers)
                buffer.DelayBeforeApplyTimer.Tick(time);

            for (int i = 0; i < _buffers.Count; i++)
                if (_buffers[i].DelayBeforeApplyTimer.TimerIsEnd)
                {
                    TryStick(_buffers[i]);
                    _buffers.RemoveAt(i);
                    i--;
                }
        }

        private void TryStick(BufferBeforeApplyStick buffer)
        {
            buffer.Effectable.EffectsProcessor.ApplyEffect(EffectType.StickyHoney, true);
            
            if (buffer.Effectable.Affiliation == Affiliation)
                buffer.Effectable.EffectsProcessor.ApplyEffect(EffectType.MoveSpeedUp, true);
            else
                buffer.Effectable.EffectsProcessor.ApplyEffect(EffectType.MoveSpeedDown, true);
        }
        
        private void OnUnitEnter(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IEffectable effectable))
            {
                if(!_buffers.Contains(c => c.Effectable == effectable))
                    _buffers.Add(new BufferBeforeApplyStick(effectable, config.DelayBeforeApply));
            }
        }

        private void OnUnitExit(ITriggerable triggerable)
        {
            if (triggerable.TryCast(out IEffectable effectable))
            {
                var index = _buffers.IndexOf(c => c.Effectable == effectable);
                if (index > -1)
                    _buffers.RemoveAt(index);
                else
                {
                    if(effectable.Affiliation == Affiliation)
                        effectable.EffectsProcessor.RemoveFixedEnter(EffectType.MoveSpeedUp);
                    else
                        effectable.EffectsProcessor.RemoveFixedEnter(EffectType.MoveSpeedDown);
                    
                    effectable.EffectsProcessor.RemoveFixedEnter(EffectType.StickyHoney);
                }
            }
        }

        private void DestructStickyTile()
        {
            var roundedPosition = MissionData.ConstructionsRepository.RoundPositionToGrid(transform.position);
            MissionData.ConstructionsRepository.GetConstruction(roundedPosition, true);
            SendDeactivateEvent();
            Destroy(gameObject);
        }
        
        private struct BufferBeforeApplyStick
        {
            public readonly IEffectable Effectable;
            public readonly Timer DelayBeforeApplyTimer;

            public BufferBeforeApplyStick(IEffectable effectable, float timer)
            {
                Effectable = effectable;
                DelayBeforeApplyTimer = new Timer(timer);
            }
        }
    }
}