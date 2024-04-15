using System;

namespace CustomTimer
{
    public interface IReadOnlyTimer
    {
        public float CurrentTime { get; }
        public float MaxTime { get; }
        
        public bool TimerIsEnd { get; }
        public bool Paused { get; }
        
        public event Action OnTimerEnd;
    }
}