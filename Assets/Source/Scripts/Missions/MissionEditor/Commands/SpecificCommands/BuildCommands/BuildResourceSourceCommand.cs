using BugStrategy.Missions.MissionEditor.Grids;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class BuildResourceSourceCommand : BuildCommand<int, ResourceSourceBase>
    {
        private readonly ResourceSourceFactory _factory;

        public BuildResourceSourceCommand(int tileId, Vector3 point, 
           ResourceSourceFactory factory, EditorResourceSourcesRepository positionsRepository) 
            : base(-1, tileId, point, positionsRepository)
        {
            _factory = factory;
        }

        protected override ResourceSourceBase Create(int id, Vector3 point)
        {
            var tile = _factory.Create(id, point);
            tile.gameObject.AddComponent<MissionEditorTileId>().Initialize(id);
            return tile;
        }

        protected override int GetId(ResourceSourceBase tile)
            => tile.GetComponent<MissionEditorTileId>().ID;

        protected override bool ReplacedIdIsValid(int replacedId)
            => replacedId >= 0;
    }
}