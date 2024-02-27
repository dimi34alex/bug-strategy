using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystemCore
{
    [Serializable]
    public struct UnitRecruitingData
    {
        [SerializeField] private UnitType currentId;
        [SerializeField] private float recruitingTime;
        [SerializeField] private int stackSize;
        [SerializeField] private float spawnPauseTime;
        [SerializeField] private SerializableDictionary<ResourceID, int> costs;

        public UnitType CurrentID => currentId;
        public float RecruitingTime => recruitingTime;
        public int StackSize => stackSize;
        public float SpawnPauseTime => spawnPauseTime;
        public IReadOnlyDictionary<ResourceID, int> Costs => costs;

        public UnitRecruitingData(UnitType id, float recruitingTime, int stackSize, float spawnPauseTime, 
            GameObject unitPrefab, SerializableDictionary<ResourceID, int> costs)
        {
            currentId = id;
            this.recruitingTime = recruitingTime;
            this.stackSize = stackSize;
            this.spawnPauseTime = spawnPauseTime;
            this.costs = costs;
        }
    }
}