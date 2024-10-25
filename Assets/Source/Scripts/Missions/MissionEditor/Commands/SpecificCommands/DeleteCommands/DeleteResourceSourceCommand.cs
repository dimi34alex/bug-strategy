using BugStrategy.Factories;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteResourceSourceCommand : DeleteCommandBase<int, ResourceSourceBase>
    {
        public DeleteResourceSourceCommand(Vector3 point, FactoryWithId<int, ResourceSourceBase> factory, 
            GridRepository<ResourceSourceBase> positionsRepository) 
            : base(point, factory, positionsRepository)
        {
        }

        protected override int GetId(ResourceSourceBase tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;
    }
}