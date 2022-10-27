using System;
using UnityEngine;

public class UnitRecruitingDataBase
{
    public float RecruitinTime => recruitinTime;
    [SerializeField] private float recruitinTime;
    public int StackSize => stackSize;
    [SerializeField] private int stackSize;
    public float SpawnPauseTime => spawnPauseTime;
    [SerializeField] private float spawnPauseTime;
    public GameObject UnitPrefab => unitPrefab;
    [SerializeField] private GameObject unitPrefab;
}
