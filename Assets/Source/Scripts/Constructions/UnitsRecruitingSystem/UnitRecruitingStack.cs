using System;
using System.Collections.Generic;
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
        public TEnum CurrentID { get; private set; }
        public Transform SpawnTransform { get; private set; }
        public GameObject UnitPrefab { get; private set; }
        public float RecruitingTime { get; private set; }
        public int StackSize { get; private set; }
        public float SpawnPauseTime { get; private set; }
        public float CurrentTime { get; private set; }
        public float SpawnPauseTimer { get; private set; }
        public int SpawnedUnits { get; private set; }
        private Dictionary<ResourceID, int> _costs;

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
            CurrentID = newData.CurrentID;
            UnitPrefab = newData.UnitPrefab;
            RecruitingTime = newData.RecruitingTime;
            StackSize = newData.StackSize;
            SpawnPauseTime = newData.SpawnPauseTime;
            CurrentTime = newData.RecruitingTime;
            SpawnPauseTimer = newData.SpawnPauseTime;
            SpawnedUnits = 0;

            _costs = new Dictionary<ResourceID, int>();
            foreach (var dataCost in newData.Costs)
                _costs.Add(dataCost.Key, dataCost.Value);
        }

        public void StackTick(float time)
        {
            if (!Empty)
            {
                CurrentTime -= time;
                if (CurrentTime <= 0)
                {
                    SpawnPauseTimer += time;
                    if (SpawnPauseTimer >= SpawnPauseTime)
                    {
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
                }
            }
        }

        public void CancelRecruiting()
        {
            if (SpawnedUnits > 0) return;

            foreach (var cost in _costs)
            {
                ResourceGlobalStorage.ChangeValue(cost.Key, cost.Value);
            }

            Empty = true;
        }
    }
}