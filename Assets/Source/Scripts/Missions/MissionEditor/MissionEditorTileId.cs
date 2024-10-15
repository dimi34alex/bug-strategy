using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorTileId : MonoBehaviour
    {
        public int ID { get; private set; }

        public void Initialize(int id) 
            => ID = id;
    }
}