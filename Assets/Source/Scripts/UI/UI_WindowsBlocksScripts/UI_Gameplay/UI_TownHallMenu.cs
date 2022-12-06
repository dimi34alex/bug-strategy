using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TownHallMenu : UIScreen
{
    [SerializeField] private UI_ERROR uiError;
    [SerializeField] private TextMeshProUGUI Alarm;

    [SerializeField] private List<TextMeshProUGUI> StackID;
    [SerializeField] private List<TextMeshProUGUI> StackTime;
    TownHall townHall;
    int currentStack = 0;
    BeeRecruitingInformation beeRecruitingInformation;

    public void _CallMenu(GameObject _townHall)
    {
        townHall = _townHall.GetComponent<TownHall>();
    }

    void Update()
    {
        beeRecruitingInformation = townHall._GetBeeRecruitingInformation(currentStack);

        if (beeRecruitingInformation.Empty)
        {
            StackID[currentStack].text = "empty";
            StackTime[currentStack].text = "";
        }
        else
        {
            StackID[currentStack].text = beeRecruitingInformation.CurrentID.ToString();
            StackTime[currentStack].text = (Mathf.Clamp((Mathf.Round(beeRecruitingInformation.CurrentTime * 100F) / 100F), 0F, Mathf.Infinity).ToString() + "/" + (Mathf.Round(beeRecruitingInformation.RecruitinTime * 100F) / 100F).ToString());
        }

        currentStack++;
        if (currentStack >= townHall.RecruitingSize || currentStack >= StackID.Count || currentStack >= StackTime.Count)
            currentStack = 0;

        if (townHall.AlarmOn)
            Alarm.text = "Bee Alarm Off";
        else
            Alarm.text = "Bee Alarm On";
    }

    public void _RecruitingWorkerBee()
    {
        uiError._ErrorCall(townHall.GetComponent<TownHall>().RecruitingWorkerBee(BeesRecruitingID.WorkerBee));
    }
    public void _WorkerBeeAlarmer()
    {
        townHall.GetComponent<TownHall>().WorkerBeeAlarmer();
    }

    public void _BuildingLVL_Up()
    {
        Debug.LogError("lvlupMenu");
        townHall.GetComponent<TownHall>().NextBuildingLevel();
    }
}
