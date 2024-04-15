using System;
using System.Collections.Generic;
using System.Linq;

namespace AttackCooldownChangerSystem
{
    public sealed class AttackCooldownChanger
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
        public void Apply(float scale)
        {
            if(scale == 0)
                return;
            
            var currentValue =  _cooldownProcessor.CurrentCapacity * (1 + scale);
            _cells.Add(new AttackCooldownChangerCell(scale));
            _cooldownProcessor.SetCooldownTime(currentValue);
        }

        public void DeApply(float scale)
        {
            var result = _cells.FindIndex(cell => Math.Abs(cell.Scale - scale) < 0.001f);
            if (result <= -1) 
                return;
            
            _cells.RemoveAt(result);
            
            var currentValue = _cooldownProcessor.CurrentCapacity / (1 + scale);
            _cooldownProcessor.SetCooldownTime(currentValue);   
        }

        public void Reset()
        {
            foreach (var cell in _cells)
                DeApply(cell.Scale);
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