using System;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionEditorUI : MonoBehaviour
    {
        public event Action<int> OnTilePressed;
        public event Action<int> OnConstructionPressed;
        public event Action<int> OnResourceSourcePressed;

        public void BuildTile(int index)
        {
            OnTilePressed?.Invoke(index);
        }
        
        public void BuildConstruction(int index)
        {
            OnConstructionPressed?.Invoke(index);
        }
        
        public void BuildResourceSource(int index)
        {
            OnResourceSourcePressed?.Invoke(index);
        }
    }
}