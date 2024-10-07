using System;
using System.Collections.Generic;
using BugStrategy.CommandsCore;
using BugStrategy.Constructions;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Commands
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

        public BuildConstructionCommand BuildConstructionCommand(ConstructionID id, AffiliationEnum affiliation, Vector3 point)
        {
            var command = new BuildConstructionCommand(id, affiliation, point, _editorConstructionsFactory, _editorConstructionsRepository);
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
            var command = new DeleteConstructionCommand(point, _editorConstructionsFactory, _editorConstructionsRepository);
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