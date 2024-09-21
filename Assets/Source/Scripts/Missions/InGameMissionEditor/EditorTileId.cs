using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class EditorTileId : MonoBehaviour
    {
        public int ID { get; private set; }

        public void Initialize(int id) 
            => ID = id;
    }
}