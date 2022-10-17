using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : BuildingBase
{
    [SerializeField] GameObject beePrefab;//префаб рабочей пчелы

    public delegate void Alarm();
    public static event Alarm WorkerBeeAlarm;

    [SerializeField] int maxNumberOfWorkerBees = 0;
    int numberOfWorkerBees = 0;

    [SerializeField] int maxTrees = 0;
    [SerializeField] int maxFlowers = 0;
    [SerializeField] int maxPlants = 0;
    int trees = 0;
    int flowers = 0;
    int plants = 0;

    void Awake()
    {
        onAwake();
        gameObject.name = "TownHall";
    }

    void Update()
    {
        onUpdate();
    }

    public void _SpawnWorkerBee()
    {
        if (numberOfWorkerBees < maxNumberOfWorkerBees)
        {
            GameObject news = Instantiate(beePrefab, transform.position, transform.rotation);
            numberOfWorkerBees++;
        }
        else
        {
            Debug.Log("Error: you have max number of workers bees");
        }
    }
    public void _AddResurse(int addTrees, int addFlowers, int addPlants)
    {
        if (trees + addTrees >= maxTrees)
            trees = maxTrees;
        else
            trees += addTrees;

        if (flowers + addFlowers >= maxFlowers)
            flowers = maxFlowers;
        else
            flowers += addFlowers;

        if (plants + addPlants >= maxPlants)
            plants = maxPlants;
        else
            plants += addPlants;
    }
    public void _WorkerBeeAlarmer()
    {
        WorkerBeeAlarm?.Invoke();
    }


    protected override void onAwake()
    {
        base.onAwake();

    }
    protected override void onUpdate()
    {
        base.onUpdate();
    }
    public override void _DestroyBuilding()
    {
        base._DestroyBuilding();
    }
    public override void _LVL_UpBuilding()
    {
        base._LVL_UpBuilding();
    }
    public override void _ReplaceBuilding()
    {
        base._ReplaceBuilding();
    }
    public override void _CallBuildingMenu(string windowName)
    {
        base._CallBuildingMenu(windowName);
    }
}
