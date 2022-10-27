using UnityEngine;

public abstract class UnitStackBase
{
    public bool Empty { get; protected set; }
    public Transform SpawnPosition { get; protected set; }
    public GameObject UnitPrefab { get; protected set; }
    public float RecruitinTime { get; protected set; }
    public int StackSize { get; protected set; }
    public float SpawnPauseTime { get; protected set; }
    public float CurrentTime { get; protected set; }
    public float SpawnPauseTimer { get; protected set; }
    public int SpawnedBees { get; protected set; }

    public UnitStackBase()
    {
        Empty = true;
    }
    public UnitStackBase(UnitRecruitingDataBase newData, Transform spawnPosition)
    {
        Empty = false;
        SpawnPosition = spawnPosition;
        UnitPrefab = newData.UnitPrefab;
        RecruitinTime = newData.RecruitinTime;
        StackSize = newData.StackSize;
        SpawnPauseTime = newData.SpawnPauseTime;
        CurrentTime = newData.RecruitinTime;
        SpawnPauseTimer = newData.SpawnPauseTime;
        SpawnedBees = 0;
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
                    if (UnitPrefab != null)
                        Object.Instantiate(UnitPrefab, SpawnPosition.position, SpawnPosition.rotation);
                    else
                        Debug.LogError("Error: prefab is null");

                    SpawnedBees++;
                    SpawnPauseTimer = 0;

                    if (SpawnedBees >= StackSize)
                        Empty = true;
                }
            }
        }
    }
}
