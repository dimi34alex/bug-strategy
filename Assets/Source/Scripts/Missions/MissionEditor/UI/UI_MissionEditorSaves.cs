using BugStrategy.Missions.MissionEditor.Saving;
using CycleFramework.Screen;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.UI
{
    public class UI_MissionEditorSaves : UIScreen
    {
        [SerializeField] private MissionEditorBuilder missionEditorBuilder;
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
            missionEditorBuilder.Load(fileName);
        }
    }
}