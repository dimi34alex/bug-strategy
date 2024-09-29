using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionEditorTileId : MonoBehaviour
    {
        public int ID { get; private set; }

        public void Initialize(int id) 
            => ID = id;
    }
}