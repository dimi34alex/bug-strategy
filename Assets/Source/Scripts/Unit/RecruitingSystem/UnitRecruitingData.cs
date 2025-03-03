using System;
using UnityEngine;

namespace BugStrategy.Unit.RecruitingSystem
{
    [Serializable]
    public class UnitRecruitingData
    {
        [SerializeField, Min(0)] private int stackSize;
        [SerializeField, Min(0)] private float recruitingTime;
        [SerializeField, Min(0)] private float spawnPauseTime;

        public float RecruitingTime => recruitingTime;
        public int StackSize => stackSize;
        public float SpawnPauseTime => spawnPauseTime;
    }
}