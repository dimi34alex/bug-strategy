using BugStrategy.Constructions;
using BugStrategy.Factory;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class DeleteConstructionCommand : DeleteCommandBase<ConstructionID, EditorConstruction>
    {
        public DeleteConstructionCommand(Vector3 point, ObjectsFactoryBase<ConstructionID, EditorConstruction> factory, 
            GridRepository<EditorConstruction> positionsRepository) 
            : base(point, factory, positionsRepository)
        {
        }

        protected override ConstructionID GetId(EditorConstruction tile) 
            => tile.constructionID;
    }
}