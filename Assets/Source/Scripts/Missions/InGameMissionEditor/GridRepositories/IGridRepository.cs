using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public interface IGridRepository
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }

        public bool FreeInExternalGrids(Vector3 position);
        
        public bool Exist(Vector3 position, bool blockIgnore = true, bool includeExternalGrids = true);
        
        public void BlockCell(Vector3 position);

        public void UnblockCell(Vector3 position);

        public bool CellIsBlocked(Vector3 position);
    }
}