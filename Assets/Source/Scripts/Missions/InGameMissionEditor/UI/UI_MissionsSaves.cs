using System.IO;
using CycleFramework.Screen;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    public class UI_MissionsSaves : UIScreen
    {
        [SerializeField] private MissionEditorBuilder missionEditorBuilder;
        [SerializeField] private MissionSaveLine missionSaveLinePrefab;
        [SerializeField] private Transform missionSaveLinesHolder;

        private void Awake()
        {
#if UNITY_EDITOR
            var directoryPath = Application.dataPath + "/Source/MissionsSaves";
#else
            var directoryPath = Application.dataPath + "/CustomMissions";
#endif
            
            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);
            
            var info = new DirectoryInfo(directoryPath);
            var fileInfo = info.GetFiles();

            foreach(var file in fileInfo)
                if (!file.Name.Contains(".meta"))
                {
                    var line = Instantiate(missionSaveLinePrefab, missionSaveLinesHolder);
                    line.Initialize(file.Name);
                    line.OnLoad += TryLoad;
                }
        }

        private void TryLoad(string fileName)
        {
            missionEditorBuilder.Load(fileName);
        }
    }
}