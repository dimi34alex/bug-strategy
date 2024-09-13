using System;
using Source.Scripts.Missions;

[Serializable]
public class GlobalData
{
    public MissionData ActiveMission { get; private set; }
    
    public GlobalData()
    {

    }
    
    public GlobalData(GlobalData globalData)
    {
        ActiveMission = globalData.ActiveMission;
    }

    public void SetActiveMission(MissionData newMission)
    {
        ActiveMission = newMission;
    }
}
