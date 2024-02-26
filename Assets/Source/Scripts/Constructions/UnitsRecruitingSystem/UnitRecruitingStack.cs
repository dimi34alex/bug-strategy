using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UnitsRecruitingSystemCore
{
    public class UnitRecruitingStack : IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; private set; } = true;
        public float RecruitingTimer  { get; private set; }
        public int SpawnedUnits { get; private set; }
        public UnitRecruitingData CurrentData { get; private set; }

        public UnitType UnitId => CurrentData.CurrentID;
        public float RecruitingTime => CurrentData.RecruitingTime;
        public int StackSize => CurrentData.StackSize;

        private float SpawnPauseTime => CurrentData.SpawnPauseTime;
        
        private Sequence _sequenceTimer;
        private IReadOnlyDictionary<ResourceID, int> Costs;
        
        public event Action<UnitType> OnSpawnUnit; 

        /// <exception cref="Exception"> Error: stack is not empty </exception>
        public void RecruitUnit(UnitRecruitingData newData)
        {
            if (!Empty) throw new Exception("Error: stack is not empty");

            Empty = false;

            CurrentData = newData;
            
            RecruitingTimer = 0;
            SpawnedUnits = 0;
            
            InvokeRecruiting();
        }
        
        private void InvokeRecruiting()
        {
            _sequenceTimer = DOTween.Sequence()
                .SetUpdate(UpdateType.Manual)
                .AppendInterval(RecruitingTime)
                .AppendCallback(SpawnUnit);
        }
        
        private void InvokeSpawnPause()
        {
            _sequenceTimer = DOTween.Sequence()
                .SetUpdate(UpdateType.Manual)
                .AppendInterval(SpawnPauseTime)
                .AppendCallback(SpawnUnit);
        }

        private void SpawnUnit()
        {
            OnSpawnUnit?.Invoke(UnitId);
                        
            SpawnedUnits++;
            if (SpawnedUnits >= StackSize)
                Empty = true;
            else
                InvokeSpawnPause();
        }
        
        public void Tick(float time)
        {
            if (Empty) return;

            RecruitingTimer = Mathf.Clamp(RecruitingTimer + time, 0, RecruitingTime);
            _sequenceTimer.ManualUpdate(time, time);
        }

        /// <returns> If cancel is possible return true, else return false </returns>
        public bool CancelRecruiting()
        {
            if (SpawnedUnits > 0) return false;
            
            _sequenceTimer.Kill();
            Empty = true;

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