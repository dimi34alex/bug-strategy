using System;

namespace CustomTimer
{
    public interface IReadOnlyTimer
    {
        public float CurrentTime { get; }
        public float MaxTime { get; }
        public float FillPercentage => CurrentTime / MaxTime;
        
        public bool TimerIsEnd { get; }
        public bool Paused { get; }

        public event Action OnTick;
        public event Action OnTimerEnd;
    }
}