using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class BuildResourceSourceCommand : BuildCommand<int, ResourceSourceBase>
    {
        public BuildResourceSourceCommand(int tileId, Vector3 point, 
           ResourceSourceFactory factory, ResourceSourceRepository positionsRepository) 
            : base(-1, tileId, point, factory, positionsRepository) { }

        protected override ResourceSourceBase Create(int id, Vector3 point)
        {
            var tile = base.Create(id, point);
            tile.gameObject.AddComponent<EditorTileId>().Initialize(id);
            return tile;
        }

        protected override int GetId(ResourceSourceBase tile)
            => tile.GetComponent<EditorTileId>().ID;

        protected override bool ReplacedIdIsValid(int replacedId)
            => replacedId >= 0;
    }
}