using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    public interface IGridBuilder
    {
        public bool IsActive { get; }
        
        public void DeActivate();
        public void ApplyBuild();
        public void Move(Vector3 point);
        public bool Clear(Vector3 point);
        public void Clear();
    }
}