using System.Collections.Generic;

namespace BugStrategy.Effects
{
    public class EffectsProcessor
    {
        private readonly IEffectable _effectable;
        private readonly EffectsFactory _effectsFactory;
        private readonly Dictionary<EffectType, EffectProcessorBase> _effectProcessors = new Dictionary<EffectType, EffectProcessorBase>();

        public IReadOnlyDictionary<EffectType, EffectProcessorBase> EffectsProcessors => _effectProcessors;

        public EffectsProcessor(IEffectable effectable, EffectsFactory effectsFactory)
        {
            _effectable = effectable;
            _effectsFactory = effectsFactory;
        }
        
        public void HandleUpdate(float time)
        {
            //вызов -> юнит апдейт -> начало цикла -> эффект апдейт -> получение урона -> смерть -> продолжение цикла
            List<EffectType> effectsToRemove = new List<EffectType>();
            foreach (var effectProcessor in _effectProcessors)
            {
                effectProcessor.Value.HandleUpdate(time);
                if(effectProcessor.Value.ExistTimer.TimerIsEnd)
                    effectsToRemove.Add(effectProcessor.Key);
            }

            foreach (var effectToRemove in effectsToRemove)
                _effectProcessors.Remove(effectToRemove);
        }

        public void ApplyEffect(EffectType effectType, bool isFixedEnter = false)
        {
            if (isFixedEnter)
            {
                if (_effectProcessors.TryGetValue(effectType, out var processor))
                {
                    processor.AddFixedEnter();
                }
                else
                {
                    if (_effectsFactory.CreateEffect(effectType, _effectable, out var newEffect))
                    {
                        _effectProcessors.Add(effectType, newEffect);
                        _effectProcessors[effectType].AddFixedEnter();
                    }
                }
            }
            else
            {
                if (_effectProcessors.TryGetValue(effectType, out var processor))
                    processor.Reset();
                else if (_effectsFactory.CreateEffect(effectType, _effectable, out var newEffect))
                    _effectProcessors.Add(effectType, newEffect);
            }
        }

        public bool HaveEffect(EffectType effectType)
            => _effectProcessors.ContainsKey(effectType);
        
        public void RemoveFixedEnter(EffectType effectType)
        {
            if(_effectProcessors.TryGetValue(effectType, out var processor))
                processor.RemoveEffectEnter();
        }
        
        public void Reset()
        {
            _effectProcessors.Clear();
        }
    }
}
