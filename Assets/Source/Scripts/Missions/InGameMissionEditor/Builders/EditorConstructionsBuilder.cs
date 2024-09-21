using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class EditorConstructionsBuilder : GridBuilder<ConstructionID, EditorConstruction>
    {
        private readonly CommandsFactory _commandsFactory;

        public EditorConstructionsBuilder(GridConfig gridConfig, GridRepository<EditorConstruction> gridRepository, 
            EditorConstructionsFactory factory, CommandsFactory commandsFactory) 
            : base(gridConfig, gridRepository, factory)
        {
            _commandsFactory = commandsFactory;
        }

        protected override ICommand CreateCommand(ConstructionID id, Vector3 point) 
            => _commandsFactory.BuildConstructionCommand(id, point);
    }
}