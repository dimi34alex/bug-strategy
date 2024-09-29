using System;
using System.Collections.Generic;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class MissionEditorCommandsFactory : ICommandsFactory
    {
        private readonly TilesFactory _tilesFactory;
        private readonly GroundPositionsRepository _groundPositionsRepository;

        private readonly EditorConstructionsFactory _editorConstructionsFactory;
        private readonly EditorConstructionsRepository _editorConstructionsRepository;
        
        private readonly ResourceSourceFactory _resourceSourceFactory;
        private readonly ResourceSourceRepository _resourceSourceRepository;

        public event Action<ICommand> OnCommandCreated;
        
        public MissionEditorCommandsFactory(TilesFactory tilesFactory, GroundPositionsRepository groundPositionsRepository, 
            EditorConstructionsFactory editorConstructionsFactory, EditorConstructionsRepository editorConstructionsRepository, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourceRepository resourceSourceRepository)
        {
            _tilesFactory = tilesFactory;
            _groundPositionsRepository = groundPositionsRepository;
            _editorConstructionsFactory = editorConstructionsFactory;
            _editorConstructionsRepository = editorConstructionsRepository;
            _resourceSourceFactory = resourceSourceFactory;
            _resourceSourceRepository = resourceSourceRepository;
        }
        
        public BuildGroundCommand BuildGroundCommand(int id, Vector3 point)
        {
            var command = new BuildGroundCommand(id, point, _tilesFactory, _groundPositionsRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }

        public BuildConstructionCommand BuildConstructionCommand(ConstructionID id, Vector3 point)
        {
            var command = new BuildConstructionCommand(id, point, _editorConstructionsFactory, _editorConstructionsRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }

        public BuildResourceSourceCommand BuildResourceSourceCommand(int id, Vector3 point)
        {
            var command = new BuildResourceSourceCommand(id, point, _resourceSourceFactory, _resourceSourceRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }

        public GenerateGroundTilesCommand GenerateGroundTilesCommand(GroundBuilder groundBuilder,
            IReadOnlyDictionary<Vector3, int> newTiles, IReadOnlyDictionary<GridKey3, int> oldTiles)
        {
            var command = new GenerateGroundTilesCommand(groundBuilder, newTiles, oldTiles);
            OnCommandCreated?.Invoke(command);
            return command;
        }
    }
}