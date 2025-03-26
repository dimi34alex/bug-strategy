using System;
using System.Collections.Generic;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.Grids;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public class MissionEditorCommandsFactory : ICommandsFactory
    {
        private readonly TilesFactory _tilesFactory;
        private readonly TilesRepository _groundPositionsRepository;

        private readonly EditorConstructionsFactory _editorConstructionsFactory;
        private readonly EditorConstructionsGrid _editorConstructionsGrid;
        
        private readonly ResourceSourceFactory _resourceSourceFactory;
        private readonly EditorResourceSourcesRepository _resourceSourceRepository;

        public event Action<ICommand> OnCommandCreated;
        
        public MissionEditorCommandsFactory(TilesFactory tilesFactory, TilesRepository groundPositionsRepository, 
            EditorConstructionsFactory editorConstructionsFactory, EditorConstructionsGrid editorConstructionsGrid, 
            ResourceSourceFactory resourceSourceFactory, EditorResourceSourcesRepository resourceSourceRepository)
        {
            _tilesFactory = tilesFactory;
            _groundPositionsRepository = groundPositionsRepository;
            _editorConstructionsFactory = editorConstructionsFactory;
            _editorConstructionsGrid = editorConstructionsGrid;
            _resourceSourceFactory = resourceSourceFactory;
            _resourceSourceRepository = resourceSourceRepository;
        }
        
        public BuildGroundCommand BuildGroundCommand(int id, Vector3 point)
        {
            var command = new BuildGroundCommand(id, point, _tilesFactory, _groundPositionsRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }

        public BuildConstructionCommand BuildConstructionCommand(ConstructionID id, AffiliationEnum affiliation, Vector3 point)
        {
            var command = new BuildConstructionCommand(id, affiliation, point, _editorConstructionsFactory, _editorConstructionsGrid);
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

        public DeleteGroundCommand DeleteGround(GridKey3 point)
        {
            var command = new DeleteGroundCommand(point, _tilesFactory, _groundPositionsRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }
        
        public DeleteConstructionCommand DeleteConstruction(GridKey3 point)
        {
            var command = new DeleteConstructionCommand(point, _editorConstructionsFactory, _editorConstructionsGrid);
            OnCommandCreated?.Invoke(command);
            return command;
        }
        
        public DeleteResourceSourceCommand DeleteResourceSource(GridKey3 point)
        {
            var command = new DeleteResourceSourceCommand(point, _resourceSourceFactory, _resourceSourceRepository);
            OnCommandCreated?.Invoke(command);
            return command;
        }
    }
}