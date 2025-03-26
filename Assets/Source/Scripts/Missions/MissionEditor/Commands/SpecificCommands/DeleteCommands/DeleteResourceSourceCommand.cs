using BugStrategy.Factories;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteResourceSourceCommand : DeleteCommandBase<int, ResourceSourceBase>
    {
        private readonly FactoryWithId<int, ResourceSourceBase> _factory;

        public DeleteResourceSourceCommand(Vector3 point, FactoryWithId<int, ResourceSourceBase> factory, 
            GridRepository<ResourceSourceBase> positionsRepository) 
            : base(point, positionsRepository)
        {
            _factory = factory;
        }

        protected override ResourceSourceBase Create(int tileId, Vector3 point) 
            => _factory.Create(tileId, point);
        
        protected override int GetId(ResourceSourceBase tile) 
            => tile.GetComponent<MissionEditorTileId>().ID;
    }
}