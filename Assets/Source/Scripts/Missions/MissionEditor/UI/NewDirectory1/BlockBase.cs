using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.UI.NewDirectory1
{
    public class BlockBase : MonoBehaviour
    {
        public void Hide() 
            => gameObject.SetActive(false);

        public void Show() 
            => gameObject.SetActive(true);
    }
}