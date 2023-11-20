using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    [Serializable]
    public struct UnitRecruitingData<TEnum> where TEnum : Enum
    {
        [SerializeField] private TEnum currentId;
        [SerializeField] private float recruitingTime;
        [SerializeField] private int stackSize;
        [SerializeField] private float spawnPauseTime;
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private SerializableDictionary<ResourceID, int> costs;

        public TEnum CurrentID => currentId;
        public float RecruitingTime => recruitingTime;
        public int StackSize => stackSize;
        public float SpawnPauseTime => spawnPauseTime;
        public GameObject UnitPrefab => unitPrefab;
        public IReadOnlyDictionary<ResourceID, int> Costs => costs;

        public UnitRecruitingData(TEnum id, float recruitingTime, int stackSize, float spawnPauseTime, 
            GameObject unitPrefab, SerializableDictionary<ResourceID, int> costs)
        {
            currentId = id;
            this.recruitingTime = recruitingTime;
            this.stackSize = stackSize;
            this.spawnPauseTime = spawnPauseTime;
            this.unitPrefab = unitPrefab;
            this.costs = costs;
        }
    }
}