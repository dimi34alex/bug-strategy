using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.GridRepositories
{
    public class GridBlockChecker
    {
        private readonly IGridRepository[] _gridRepositories;
     
        public GridBlockChecker(IGridRepository gridRepository) 
            => _gridRepositories = new[] { gridRepository };

        public GridBlockChecker(IGridRepository[] gridRepositories) 
            => _gridRepositories = gridRepositories;

        public bool Blocked(Vector3 position)
        {
            foreach (var gridRepository in _gridRepositories)
                if (gridRepository.Exist(position, false, false))
                    return true;

            return false;
        }
    }
}