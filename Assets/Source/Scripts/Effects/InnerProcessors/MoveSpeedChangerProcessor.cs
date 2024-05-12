using System;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Unit.Effects.InnerProcessors
{
    public class MoveSpeedChangerProcessor
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly List<MoveSpeedChangerCell> _cells;

        public MoveSpeedChangerProcessor(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _cells = new List<MoveSpeedChangerCell>();
        }

        /// <param name="scale">
        ///     Positive scale mean increase <br/>
        ///     Negative scale mean decrease <br/>
        ///     If scale == 0, then value dont change
        /// </param>
        public void ApplyEffect(float scale)
        {
            if(scale == 0)
                return;
            
            var currentValue =  _navMeshAgent.speed * (1 + scale);
            _cells.Add(new MoveSpeedChangerCell(scale));
            _navMeshAgent.speed = currentValue;
        }

        public void RemoveEffect(float scale)
        {
            var foundIndex = _cells.FindIndex(cell => Math.Abs(cell.Scale - scale) < 0.001f);
            if (foundIndex <= -1) 
                return;
            
            _cells.RemoveAt(foundIndex);
            
            _navMeshAgent.speed = _navMeshAgent.speed / (1 + scale);
        }

        /// <summary>
        /// Remove all effects and set default move speed
        /// </summary>
        public void Reset()
        {
            for (int i = _cells.Count - 1; i >= 0; i--)
                RemoveEffect(_cells[i].Scale);
        }
        
        private struct MoveSpeedChangerCell
        {
            public readonly float Scale;

            public MoveSpeedChangerCell(float scale)
            {
                Scale = scale;
            }
        }
    }
}