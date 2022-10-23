using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TownHallMenu : UIScreen
{
    [SerializeField] private TextMeshProUGUI Alarm;
    [SerializeField] private TextMeshProUGUI BeesNumber;
    [SerializeField] private TextMeshProUGUI BeesQueue;
    [SerializeField] private TextMeshProUGUI SpawnTimer;
    TownHall townHall;

    public void _CallMenu(GameObject _townHall)
    {
        townHall = _townHall.GetComponent<TownHall>();
    }

    void Update()
    {
        BeesNumber.text = townHall.WorkerBeesNumber.ToString() + "/" + townHall.MaxWorkerBeesNumber.ToString();
        BeesQueue.text = townHall.WorkerBeesQueue.ToString() + "/" + townHall.MaxWorkerBeesQueue.ToString();

        if (townHall.AlarmOn)
        {
            Alarm.text = "Bee Alarm Off";
        }
        else
        {
            Alarm.text = "Bee Alarm On";
        }
    }

    public void _SpawnWorkerBee()
    {
        townHall.GetComponent<TownHall>()._SpawnWorkerBee();
    }
    public void _WorkerBeeAlarmer()
    {
        townHall.GetComponent<TownHall>()._WorkerBeeAlarmer();
    }

    public void _BuildingLVL_Up()
    {
        townHall.GetComponent<TownHall>()._NextBuildingLevel();
    }
}
