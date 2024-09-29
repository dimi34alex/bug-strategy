using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class EditorConstructionsBuilder : GridBuilder<ConstructionID, EditorConstruction>
    {
        private readonly MissionEditorCommandsFactory _missionEditorCommandsFactory;

        public EditorConstructionsBuilder(GridConfig gridConfig, GridRepository<EditorConstruction> gridRepository, 
            EditorConstructionsFactory factory, MissionEditorCommandsFactory missionEditorCommandsFactory) 
            : base(gridConfig, gridRepository, factory)
        {
            _missionEditorCommandsFactory = missionEditorCommandsFactory;
        }

        protected override ICommand CreateBuildCommand(ConstructionID id, Vector3 point) 
            => _missionEditorCommandsFactory.BuildConstructionCommand(id, point);
    }
}