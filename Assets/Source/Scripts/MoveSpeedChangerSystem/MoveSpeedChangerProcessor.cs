using System.Collections.Generic;
using CustomTimer;
using UnityEngine;
using UnityEngine.AI;

namespace MoveSpeedChangerSystem
{
    public class MoveSpeedChangerProcessor
    {
        private readonly ProcessorBlock _speedUpBlock;
        private readonly ProcessorBlock _speedDownBlock;
        private readonly NavMeshAgent _navMeshAgent;

        public MoveSpeedChangerProcessor(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _speedUpBlock = new ProcessorBlock(_navMeshAgent);
            _speedDownBlock = new ProcessorBlock(_navMeshAgent);
        }

        public void HandleUpdate(float time)
        {
            _speedUpBlock.HandleUpdate(time);
            _speedDownBlock.HandleUpdate(time);
        }

        public void Invoke(MoveSpeedChangerConfig moveSpeedChangerConfig, bool hardSet = false)
        {
            if (moveSpeedChangerConfig.Power >= 0)
                _speedUpBlock.Invoke(moveSpeedChangerConfig, hardSet);
            else
                _speedDownBlock.Invoke(moveSpeedChangerConfig, hardSet);
        }

        //TODO: ask Dima about working of increase and decrease speed
        /// <param name="scale">
        ///     Positive scale mean increase <br/>
        ///     Negative scale mean decrease <br/>
        ///     If scale == 0, then value dont change
        /// </param>
        public void Apply(float scale)
            => _navMeshAgent.speed = _navMeshAgent.speed / (1 + scale);
        //TODO: ask Dima about working of increase and decrease speed
        public void DeApply(float scale)
            => _navMeshAgent.speed = _navMeshAgent.speed * (1 + scale);
        
        private class ProcessorBlock
        {
            private readonly NavMeshAgent _navMeshAgent;
            private readonly List<ProcessorBlockCell> _cells;

            private ProcessorBlockCell _currentBlockCell;

            public ProcessorBlock(NavMeshAgent navMeshAgent)
            {
                _navMeshAgent = navMeshAgent;
                _cells = new List<ProcessorBlockCell>();
            }

            public void HandleUpdate(float time)
            {
                foreach (var cell in _cells)
                    cell.HandleUpdate(time);
            }
            
            public void Invoke(MoveSpeedChangerConfig moveSpeedChangerConfig, bool hardSet)
            {
                foreach (var cell in _cells)
                    if (cell.Power == moveSpeedChangerConfig.Power)
                    {
                        cell.TrySetTime(moveSpeedChangerConfig.Time, hardSet);
                        return;
                    }

                var processorBlockCell = new ProcessorBlockCell(_navMeshAgent, moveSpeedChangerConfig);
                processorBlockCell.Timer.OnTimerEnd += Recheck;
                _cells.Add(processorBlockCell);

                if (_currentBlockCell == null)
                {
                    _currentBlockCell = processorBlockCell;
                    _currentBlockCell.Activate();
                    return;
                }

                if (Mathf.Abs(_currentBlockCell.Power) < Mathf.Abs(processorBlockCell.Power))
                {
                    _currentBlockCell.DeActivate();
                    _currentBlockCell = processorBlockCell;
                    _currentBlockCell.Activate();
                }
            }

            private void Recheck()
            {
                for (int i = 0; i < _cells.Count; i++)
                {
                    if (_cells[i].Timer.TimerIsEnd)
                    {
                        _cells[i].DeActivate();
                        _cells.RemoveAt(i);
                        i--;
                    }
                }

                if (!_currentBlockCell.Timer.TimerIsEnd)
                    return;

                _currentBlockCell = null;
                var currentIndex = -1;
                var currentPower = 0;

                for (int i = 0; i < _cells.Count; i++)
                {
                    if (Mathf.Abs(_cells[i].Power) > Mathf.Abs(currentPower))
                    {
                        currentIndex = i;
                        currentPower = _cells[i].Power;
                    }
                }

                if (currentIndex > -1)
                {
                    _currentBlockCell = _cells[currentIndex];
                    _currentBlockCell.Activate();
                }
            }

            private class ProcessorBlockCell
            {
                public readonly int Power;
                public readonly Timer Timer;

                private readonly NavMeshAgent _navMeshAgent;
                private readonly float _changeScale;
                private bool _isActive;

                public ProcessorBlockCell(NavMeshAgent navMeshAgent, MoveSpeedChangerConfig moveSpeedChangerConfig)
                {
                    _navMeshAgent = navMeshAgent;
                    Power = moveSpeedChangerConfig.Power;
                    _changeScale = (float)Power / 100;
                    Timer = new Timer(moveSpeedChangerConfig.Time);
                }

                public void HandleUpdate(float time)
                    => Timer.Tick(time);

                public void Activate()
                {
                    if (_isActive)
                        return;

                    _isActive = true;
                    _navMeshAgent.speed = _navMeshAgent.speed / (1 + _changeScale);
                }

                public void DeActivate()
                {
                    if (!_isActive)
                        return;

                    _isActive = false;
                    _navMeshAgent.speed = _navMeshAgent.speed * (1 + _changeScale);
                }
                
                public void TrySetTime(float time, bool hardSet)
                {
                    if (hardSet || Timer.MaxTime - Timer.CurrentTime < time)
                        Timer.SetMaxValue(time);
                }
            }
        }
    }
}