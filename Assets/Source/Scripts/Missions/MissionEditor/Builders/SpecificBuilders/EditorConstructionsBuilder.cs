using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Grids;
using BugStrategy.Missions.MissionEditor.Commands;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.Saving;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    public class EditorConstructionsBuilder : GridBuilder<(ConstructionID, AffiliationEnum), EditorConstruction>
    {
        private readonly MissionEditorCommandsFactory _commandsFactory;
        private readonly EditorConstructionsFactory _factory;

        public EditorConstructionsBuilder(GridConfig gridConfig, GridRepository<EditorConstruction> gridRepository, 
            EditorConstructionsFactory factory, MissionEditorCommandsFactory commandsFactory) 
            : base(gridConfig, gridRepository)
        {
            _commandsFactory = commandsFactory;
            _factory = factory;
        }

        protected override EditorConstruction CreateMovableModel((ConstructionID, AffiliationEnum) id, Vector3 position) 
            => _factory.Create(id.Item1, position);

        protected override ICommand CreateBuildCommand((ConstructionID, AffiliationEnum) id, Vector3 point)
            => _commandsFactory.BuildConstructionCommand(id.Item1, id.Item2, point);
        
        protected override ICommand CreateDeleteCommand(GridKey3 point)
            => _commandsFactory.DeleteConstruction(point);
        
        public async Task LoadGroundTiles(CancellationToken cancellationToken, IReadOnlyList<Mission.ConstructionPair> groundTiles)
        {
            for (int i = 0; i < groundTiles.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(5, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                var tile = _factory.Create(groundTiles[i].Id, groundTiles[i].Position);
                tile.Initialize(groundTiles[i].Affiliation);
                
                GridRepository.Add(groundTiles[i].Position, tile);
            }
        }
    }
}