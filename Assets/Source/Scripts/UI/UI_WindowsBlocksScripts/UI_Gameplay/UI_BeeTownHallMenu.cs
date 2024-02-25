using System.Collections.Generic;
using Constructions;
using UnityEngine;
using TMPro;
using UnitsRecruitingSystem;

public class UI_BeeTownHallMenu : UI_EvolveConstructionScreenBase<BeeTownHall>
{
    [SerializeField] private TextMeshProUGUI alarm;
    [SerializeField] private List<TextMeshProUGUI> stackID;
    [SerializeField] private List<TextMeshProUGUI> stackTime;
    
    private IReadOnlyUnitsRecruiter<BeesRecruitingID> _recruiter;
    
    public void _CallMenu(ConstructionBase townHall)
    {
        _construction = townHall.Cast<BeeTownHall>();
        ChangeAlarmDisplay();
        
        if(!(_recruiter is null)) 
            _recruiter.OnChange -= UpdateRecruitInfo;
        _recruiter = _construction.Recruiter;
        _recruiter.OnChange += UpdateRecruitInfo;
        UpdateRecruitInfo();
    }
    
    private void UpdateRecruitInfo()
    {
        var beeRecruitingInformation = _recruiter.GetRecruitingInformation();
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
                float fullTime = Mathf.Round(beeRecruitingInformation[n].RecruitingTime * 100F) / 100F;
                stackTime[n].text = (currentTime + "/" + fullTime);
            }
        }
    }

    public void _RecruitingWorkerBee()
    {
        _construction.RecruitingWorkerBee(BeesRecruitingID.WorkerBee);
    }
    
    public void _WorkerBeeAlarmer()
    {
        _construction.WorkerBeeAlarmer();
        ChangeAlarmDisplay();
    }

    private void ChangeAlarmDisplay()
    {
        if (_construction.AlarmIsOn)
            alarm.text = "Bee Alarm Off";
        else
            alarm.text = "Bee Alarm On";
    }

    private void OnDisable()
    {
        _recruiter.OnChange -= UpdateRecruitInfo;
    }
}
