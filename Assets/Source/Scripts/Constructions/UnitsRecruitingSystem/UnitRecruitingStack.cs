using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    public interface IReadOnlyUnitRecruitingStack<TEnum>
        where TEnum : Enum
    {
        public bool Empty { get; }
        public TEnum CurrentID { get; }
        public Transform SpawnTransform { get; }
        public GameObject UnitPrefab { get; }
        public float RecruitingTime { get; }
        public int StackSize { get; }
        public float SpawnPauseTime { get; }
        public float CurrentTime { get; }
        public float SpawnPauseTimer { get; }
        public int SpawnedUnits { get; }
    }

    public class UnitRecruitingStack<TEnum> : IReadOnlyUnitRecruitingStack<TEnum>
        where TEnum : Enum
    {
        public bool Empty { get; private set; }
        
        private UnitRecruitingData<TEnum> _unitRecruitingData;
        
        public TEnum CurrentID => _unitRecruitingData.CurrentID;
        public GameObject UnitPrefab => _unitRecruitingData.UnitPrefab;
        public float RecruitingTime => _unitRecruitingData.RecruitingTime;
        public int StackSize => _unitRecruitingData.StackSize;
        public float SpawnPauseTime => _unitRecruitingData.SpawnPauseTime;
        private IReadOnlyDictionary<ResourceID, int> Costs => _unitRecruitingData.Costs;
        
        public Transform SpawnTransform { get; private set; }
        public float CurrentTime  { get; private set; }
        public float SpawnPauseTimer { get; private set; }

        public int SpawnedUnits { get; private set; }
        
        public UnitRecruitingStack(Transform spawnTransform)
        {
            Empty = true;
            SpawnTransform = spawnTransform;
        }

        public void SetNewData(UnitRecruitingData<TEnum> newData)
        {
            if (!Empty)
            {
                Debug.LogError("Stack is not empty");
                return;
            }

            if (newData.UnitPrefab is null) throw new Exception("Error: prefab is null");

            Empty = false;
            
            _unitRecruitingData = newData;
            CurrentTime = 0;
            SpawnPauseTimer = newData.SpawnPauseTime;
            
            SpawnedUnits = 0;
        }

        public void StackTick(float time)
        {
            if (Empty) return;

            if (CurrentTime < RecruitingTime)
            {
                CurrentTime += time;
                return;
            }

            if (SpawnPauseTimer < SpawnPauseTime)
            {
                SpawnPauseTimer += time;
                return;
            }

            Vector3 spawnPos = SpawnTransform.position;
            float randomPosition = UnityEngine.Random.Range(-0.01f, 0.01f);

            UnityEngine.Object.Instantiate(UnitPrefab,
                new Vector3(spawnPos.x + randomPosition, spawnPos.y, spawnPos.z + randomPosition),
                SpawnTransform.rotation);
                        
            SpawnedUnits++;
            SpawnPauseTimer = 0;

            if (SpawnedUnits >= StackSize)
                Empty = true;
        }

        public void CancelRecruiting()
        {
            if (SpawnedUnits > 0) return;

            foreach (var cost in Costs)
                ResourceGlobalStorage.ChangeValue(cost.Key, cost.Value);

            Empty = true;
        }
    }
}