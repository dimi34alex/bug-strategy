using BugStrategy.Constructions;
using BugStrategy.Factories;
using BugStrategy.Grids;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteConstructionCommand : DeleteCommandBase<ConstructionID, EditorConstruction>
    {
        private readonly FactoryWithId<ConstructionID, EditorConstruction> _factory;

        public DeleteConstructionCommand(Vector3 point, FactoryWithId<ConstructionID, EditorConstruction> factory, 
            GridRepository<EditorConstruction> positionsRepository) 
            : base(point, positionsRepository)
        {
            _factory = factory;
        }

        protected override EditorConstruction Create(ConstructionID tileId, Vector3 point) 
            => _factory.Create(tileId, point);

        protected override ConstructionID GetId(EditorConstruction tile) 
            => tile.constructionID;
    }
}