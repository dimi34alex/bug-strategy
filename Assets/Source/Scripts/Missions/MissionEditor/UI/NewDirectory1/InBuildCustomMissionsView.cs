using BugStrategy.Missions.MissionEditor.Saving;
using CycleFramework.Execute;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class InBuildCustomMissionsView : MonoBehaviour
    {
        [SerializeField] private MissionSaveLine missionSaveLinePrefab;
        [SerializeField] private Transform missionSaveLinesHolder;

        private void Awake()
        {
            var filesNames = MissionSaveAndLoader.GetAllMissionsNames();
            foreach(var fileName in filesNames)
                if (!fileName.Contains(".meta"))
                {
                    var line = Instantiate(missionSaveLinePrefab, missionSaveLinesHolder);
                    line.Initialize(fileName);
                    line.OnLoad += TryLoad;
                }
        }

        private void TryLoad(string fileName)
        {
            GlobalDataHolder.GlobalData.SetActiveMission(1);
        }
    }
}