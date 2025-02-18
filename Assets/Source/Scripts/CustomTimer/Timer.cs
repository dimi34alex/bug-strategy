﻿using System;
using UnityEngine;

namespace BugStrategy.CustomTimer
{
    public class Timer : IReadOnlyTimer
    {
        public float CurrentTime { get; private set; }
        public float MaxTime { get; private set; }

        public bool TimerIsEnd { get; private set; }
        public bool Paused { get; private set; }
        public bool IsActive => !TimerIsEnd && !Paused;

        public event Action OnTick;
        public event Action OnTimerEnd;
        
        public Timer(float maxValue, float startValue = 0, bool paused = false)
        {
            MaxTime = maxValue;
            CurrentTime = startValue;
            Paused = paused;
            
            if (CurrentTime >= MaxTime)
                TimerIsEnd = true;
        }
        
        public void SetMaxValue(float newMaxValue, bool reset = true, bool saveCurrentTime = false)
        {
            MaxTime = newMaxValue;

            float currentTime = 0;
            if (saveCurrentTime)
                currentTime = CurrentTime;
            
            if (reset)
                Reset();

            CurrentTime = currentTime;
        }

        public void Reset(bool paused = false)
        {
            TimerIsEnd = false;
            CurrentTime = 0;
            Paused = paused;
        }
        
        public void SetPause() => Paused = true;
        
        public void Continue() => Paused = false;

        public void SetCurrentTIme(float currentTime)
        {
            CurrentTime = currentTime;
            
            if (CurrentTime >= MaxTime)
            {
                TimerIsEnd = true;
                OnTimerEnd?.Invoke();
            }
        }
        
        public void Tick(float time)
        {
            if(TimerIsEnd || Paused) return;
            
            UpdateTimer(time);
            OnTick?.Invoke();
        }

        private void UpdateTimer(float time)
        {
            CurrentTime += time;
            CurrentTime = Mathf.Clamp(CurrentTime, 0, MaxTime);
            
            if (CurrentTime >= MaxTime)
            {
                TimerIsEnd = true;
                OnTimerEnd?.Invoke();
            }
        }
    }
}