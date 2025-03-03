using System;
using System.Collections.Generic;
using BugStrategy.CustomTimer;
using BugStrategy.ResourcesSystem;

namespace BugStrategy.Unit.RecruitingSystem
{
    public class UnitRecruitingStack : IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; private set; } = true;
        public int SpawnedUnits { get; private set; }
        public UnitType UnitId { get; private set; }
        public UnitRecruitingData CurrentData { get; private set; }
        public IReadOnlyDictionary<ResourceID, int> Costs { get; private set; }
        
        public float RecruitingTime => CurrentData.RecruitingTime;
        public float SpawnPauseTime => CurrentData.SpawnPauseTime;
        public int StackSize => CurrentData.StackSize;
        public float RecruitingTimer => _recruitingTimer.CurrentTime;

        private readonly Timer _recruitingTimer;
        private readonly Timer _spawnPauseTimer;

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
        public void RecruitUnit(UnitType unitType, UnitRecruitingData newData, 
            IReadOnlyDictionary<ResourceID, int> cost)
        {
            if (!Empty) 
                throw new Exception("Error: stack is not empty");

            Empty = false;
            UnitId = unitType;
            CurrentData = newData;
            Costs = cost;
            SpawnedUnits = 0;

            _recruitingTimer.SetMaxValue(RecruitingTime);
            _spawnPauseTimer.SetMaxValue(SpawnPauseTime, false);
        }
        
        private void SpawnUnit()
        {
            SpawnedUnits++;
            if (SpawnedUnits >= StackSize)
            {
                Empty = true;
                OnSpawnUnit?.Invoke(UnitId);
                OnBecameEmpty?.Invoke();
            }
            else
            {
                OnSpawnUnit?.Invoke(UnitId);
                _spawnPauseTimer.Reset();
            }
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
}