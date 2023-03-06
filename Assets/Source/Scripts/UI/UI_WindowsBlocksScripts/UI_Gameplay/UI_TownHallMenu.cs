using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TownHallMenu : UIScreen
{
    [SerializeField] private UI_ERROR uiError;
    [SerializeField] private TextMeshProUGUI alarm;

    [SerializeField] private List<TextMeshProUGUI> stackID;
    [SerializeField] private List<TextMeshProUGUI> stackTime;
    TownHall _townHall;
    
    void Update()
    {
        Displaying();
    }

    private void Displaying()
    {
        List<BeeRecruitingInformation> beeRecruitingInformation = _townHall.GetRecruitingInformation();

        for (int n = 0; n < beeRecruitingInformation.Count && n < stackID.Count && n < stackTime.Count; n++)
        {
            if (beeRecruitingInformation[n].Empty)
            {
                stackID[n].text = "empty";
                stackTime[n].text = "";
            }
            else
            {
                stackID[n].text = beeRecruitingInformation[n].CurrentID.ToString();
                float currentTime = Mathf.Clamp((Mathf.Round(beeRecruitingInformation[n].CurrentTime * 100F) / 100F), 0F, Mathf.Infinity);
                float fullTime = Mathf.Round(beeRecruitingInformation[n].RecruitinTime * 100F) / 100F;
                stackTime[n].text = (currentTime + "/" + fullTime);
            }
        }
        
        if (_townHall.AlarmOn)
            alarm.text = "Bee Alarm Off";
        else
            alarm.text = "Bee Alarm On";
    }
    
    public void _CallMenu(GameObject townHall)
    {
        this._townHall = townHall.GetComponent<TownHall>();
    }
    
    public void _BuildingLVL_Up()
    {
        _townHall.NextBuildingLevel();
    }

    public void _RecruitingWorkerBee()
    {
        uiError._ErrorCall(_townHall.RecruitingWorkerBee(BeesRecruitingID.WorkerBee));
    }
    
    public void _WorkerBeeAlarmer()
    {
        _townHall.WorkerBeeAlarmer();
    }
}
