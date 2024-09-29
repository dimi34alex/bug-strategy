using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    public class MissionSaveLine : MonoBehaviour
    {
        [SerializeField] private TMP_Text missionName;
        [SerializeField] private Button loadButton;

        /// <summary>
        /// return mission file name
        /// </summary>
        public event Action<string> OnLoad;
        
        public void Initialize(string fileName)
        {
            missionName.text = fileName;
            loadButton.onClick.AddListener(Load);
        }

        private void Load() 
            => OnLoad?.Invoke(missionName.text);
    }
}