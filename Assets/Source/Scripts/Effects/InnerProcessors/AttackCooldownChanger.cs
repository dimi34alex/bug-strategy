using System;
using System.Collections.Generic;

namespace Unit.Effects.InnerProcessors
{
    public class AttackCooldownChanger
    {
        private readonly CooldownProcessor _cooldownProcessor;
        private readonly List<AttackCooldownChangerCell> _cells;
        
        public AttackCooldownChanger(CooldownProcessor cooldownProcessor)
        {
            _cooldownProcessor = cooldownProcessor;
            _cells = new List<AttackCooldownChangerCell>();
        }
        
        /// <param name="scale">
        ///     Positive scale mean decrease <br/>
        ///     Negative scale mean increase <br/>
        ///     If scale == 0, then value dont change
        /// </param>
        public void ApplyEffect(float scale)
        {
            if(scale == 0)
                return;
            
            var currentValue =  _cooldownProcessor.CurrentCapacity * (1 + scale);
            _cells.Add(new AttackCooldownChangerCell(scale));
            _cooldownProcessor.SetCooldownTime(currentValue);
        }

        public void RemoveEffect(float scale)
        {
            var foundIndex = _cells.FindIndex(cell => Math.Abs(cell.Scale - scale) < 0.001f);
            if (foundIndex <= -1) 
                return;
            
            _cells.RemoveAt(foundIndex);
            
            var currentValue = _cooldownProcessor.CurrentCapacity / (1 + scale);
            _cooldownProcessor.SetCooldownTime(currentValue);   
        }

        /// <summary>
        /// Remove but not revert effects, you need manual reset cooldown processor
        /// </summary>
        public void Clear()
        {
            _cells.Clear();
        }
        
        private struct AttackCooldownChangerCell
        {
            public readonly float Scale;

            public AttackCooldownChangerCell(float scale)
            {
                Scale = scale;
            }
        }
    }
}