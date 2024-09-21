using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class EditorConstructionsBuilder : GridBuilder<ConstructionID, EditorConstruction>
    {
        private readonly EditorConstructionsFactory _editorConstructionsFactory;

        public EditorConstructionsBuilder(GridConfig gridConfig, GridRepository<EditorConstruction> gridRepository, 
            EditorConstructionsFactory editorConstructionsFactory) 
            : base(gridConfig, gridRepository)
        {
            _editorConstructionsFactory = editorConstructionsFactory;
        }

        protected override EditorConstruction Create(ConstructionID id, Vector3 point = default) 
            => _editorConstructionsFactory.Create(id, point);
    }
}