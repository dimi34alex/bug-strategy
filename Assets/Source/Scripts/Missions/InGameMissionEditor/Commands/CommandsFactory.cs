using System;
using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class CommandsFactory
    {
        private readonly TilesFactory _tilesFactory;
        private readonly TilesPositionsRepository _tilesPositionsRepository;

        private readonly EditorConstructionsFactory _editorConstructionsFactory;
        private readonly EditorConstructionsRepository _editorConstructionsRepository;
        
        private readonly ResourceSourceFactory _resourceSourceFactory;
        private readonly ResourceSourceRepository _resourceSourceRepository;

        public event Action<ICommand> OnCommandCreated;
        
        public CommandsFactory(TilesFactory tilesFactory, TilesPositionsRepository tilesPositionsRepository, 
            EditorConstructionsFactory editorConstructionsFactory, EditorConstructionsRepository editorConstructionsRepository, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourceRepository resourceSourceRepository)
        {
            _tilesFactory = tilesFactory;
            _tilesPositionsRepository = tilesPositionsRepository;
            _editorConstructionsFactory = editorConstructionsFactory;
            _editorConstructionsRepository = editorConstructionsRepository;
            _resourceSourceFactory = resourceSourceFactory;
            _resourceSourceRepository = resourceSourceRepository;
        }
        
        public BuildTileCommand BuildTileCommand(int id, Vector3 point)
        {
            var command = new BuildTileCommand(id, point, _tilesFactory, _tilesPositionsRepository);
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
    }
}