using System;
using System.Collections.Generic;
using BugStrategy.CustomTimer;
using BugStrategy.ResourcesSystem;
using BugStrategy.Unit;

namespace BugStrategy.Constructions.UnitsRecruitingSystem
{
    public class UnitRecruitingStack : IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; private set; } = true;
        public int SpawnedUnits { get; private set; }
        public UnitRecruitingData CurrentData { get; private set; }
        
        public UnitType UnitId => CurrentData.CurrentID;
        public float RecruitingTime => CurrentData.RecruitingTime;
        public float SpawnPauseTime => CurrentData.SpawnPauseTime;
        public int StackSize => CurrentData.StackSize;
        public float RecruitingTimer => _recruitingTimer.CurrentTime;

        private readonly Timer _recruitingTimer;
        private readonly Timer _spawnPauseTimer;
        private IReadOnlyDictionary<ResourceID, int> _costs;
        
        public event Action<UnitType> OnSpawnUnit;
        public event Action OnBecameEmpty; 

        public UnitRecruitingStack()
        {
            _recruitingTimer = new Timer(0);
            _spawnPauseTimer = new Timer(0);

            _recruitingTimer.OnTimerEnd += SpawnUnit;
            _spawnPauseTimer.OnTimerEnd += SpawnUnit;
        }
        
        /// <exception cref="Exception"> Error: stack is not empty </exception>
        public void RecruitUnit(UnitRecruitingData newData)
        {
            if (!Empty) 
                throw new Exception("Error: stack is not empty");

            Empty = false;
            CurrentData = newData;
            SpawnedUnits = 0;
            
            _recruitingTimer.SetMaxValue(RecruitingTime);
            _spawnPauseTimer.SetMaxValue(SpawnPauseTime, false);
        }
        
        private void SpawnUnit()
        {
            OnSpawnUnit?.Invoke(UnitId);
                        
            SpawnedUnits++;
            if (SpawnedUnits >= StackSize)
            {
                Empty = true;
                OnBecameEmpty?.Invoke();
            }
            else
                _spawnPauseTimer.Reset();
        }
        
        public void Tick(float time)
        {
            if (Empty) return;

            _recruitingTimer.Tick(time);
            _spawnPauseTimer.Tick(time);
        }

        /// <returns> If cancel is possible return true, else return false </returns>
        public bool CancelRecruiting()
        {
            if (SpawnedUnits > 0) 
                return false;
            
            Empty = true;
            _recruitingTimer.SetPause();
            _spawnPauseTimer.SetPause();

            return true;
        }
    }
    
    public interface IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; }
        public UnitType UnitId { get; }
        public float RecruitingTime { get; }
        public float RecruitingTimer { get; }
        public int StackSize { get; }
        public int SpawnedUnits { get; }
    }
}