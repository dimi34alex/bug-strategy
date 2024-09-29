using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    public class MissionNameInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button saveButton;

        [SerializeField] private MissionEditorBuilder missionEditorBuilder;
        
        private void Awake()
        {
            saveButton.onClick.AddListener(SaveMission);
        }

        private void SaveMission()
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                Debug.LogError("Input null or empty"); 
                return;
            }
            
            missionEditorBuilder.Save(inputField.text);
        }
    }
}