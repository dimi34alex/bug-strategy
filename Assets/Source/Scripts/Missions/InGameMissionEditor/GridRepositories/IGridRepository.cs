using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public interface IGridRepository
    {
        public IReadOnlyCollection<GridKey3> Positions { get; }

        public void SetExternalGrids(IGridRepository[] externalGrids);

        public bool FreeInExternalGrids(Vector3 position);
        
        public bool Exist(Vector3 position, bool includeBlockedCells = true, bool includeExternalGrids = true);
        
        public void BlockCell(Vector3 position);

        public void UnblockCell(Vector3 position);

        public bool CellIsBlocked(Vector3 position);
    }

    public interface IGridRepository<TValue> : IGridRepository
    {
        public IReadOnlyDictionary<GridKey3, TValue> Tiles { get; }

        public bool TryAdd(Vector3 position, TValue tile);

        public void Add(Vector3 position, TValue tile);

        public TValue Get(Vector3 position, bool withRemove = false);
    }
}