using System;
using UnityEngine;

[Serializable]
public class GlobalData
{
    public int ActiveMissionIndex { get; private set; }
    
    public GlobalData()
    {

    }
    
    public GlobalData(GlobalData globalData)
    {
        ActiveMissionIndex = globalData.ActiveMissionIndex;
    }

    public void SetActiveMission(int newMissionIndex)
    {
        if (newMissionIndex < 0)
        {
            Debug.LogError($"Mission Index cant be less then 0");
            newMissionIndex = 0;
        }
        
        ActiveMissionIndex = newMissionIndex;
    }
}
