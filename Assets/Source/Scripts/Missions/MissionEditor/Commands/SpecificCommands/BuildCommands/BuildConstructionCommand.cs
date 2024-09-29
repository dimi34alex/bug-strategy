using BugStrategy.Constructions;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class BuildConstructionCommand : BuildCommand<ConstructionID, EditorConstruction>
    {
        public BuildConstructionCommand(ConstructionID tileId, Vector3 point, 
            EditorConstructionsFactory factory, EditorConstructionsRepository positionsRepository) 
            : base(ConstructionID.None, tileId, point, factory, positionsRepository) { }

        protected override ConstructionID GetId(EditorConstruction tile) 
            => tile.constructionID;

        protected override bool ReplacedIdIsValid(ConstructionID replacedId) 
            => replacedId != ConstructionID.None;
    }
}