using System;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.GridRepositories
{
    public interface IGridProvider
    {
        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;
    }
}