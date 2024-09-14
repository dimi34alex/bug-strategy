using System;
using BugStrategy.CustomTimer;
using UnityEngine;

namespace BugStrategy.Effects
{
    public abstract class EffectProcessorBase
    {
        private readonly Timer _existTimer;
        private int _fixedEnters;

        public IReadOnlyTimer ExistTimer => _existTimer;
        public abstract EffectType EffectType { get; }

        protected event Action<float> UpdateEvent;
        
        protected EffectProcessorBase(float existTime)
        {
            _existTimer = existTime <= 0 
                    ? new Timer(0.0001f) 
                    : new Timer(existTime);
        }
        
        public void HandleUpdate(float time)
        {
            UpdateEvent?.Invoke(time);
            if(_fixedEnters <= 0)
                _existTimer.Tick(time);
        }

        public void AddFixedEnter()
        {
            ++_fixedEnters;
        }

        public void Reset()
        {
            _existTimer.Reset();
        }
        
        public void RemoveEffectEnter()
        {
            var prevStaticEnterValue = _fixedEnters;
            --_fixedEnters;

#if UNITY_EDITOR || DEVELOPMENT_BUILD	
            if(_fixedEnters < 0)
                Debug.LogWarning($"Attention!: fixed enter less then 0: {_fixedEnters}");   
#endif
            
            if(prevStaticEnterValue > 0 && _fixedEnters <= 0)
                _existTimer.Reset();
        }
    }
}