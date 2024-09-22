using System;
using System.Collections.Generic;
using System.Linq;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Tiles;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class GenerateGroundTilesCommand : ICommand
    {
        public bool IsExecuted { get; private set; }

        private readonly TilesFactory _factory;
        private readonly IReadOnlyDictionary<Vector3, int> _newTiles;
        private readonly IReadOnlyDictionary<GridKey3, int> _oldTiles;
        private readonly TilesPositionsRepository _tilesPositionsRepository;
        
        public event Action<ICommand> OnExecuted;
        
        public GenerateGroundTilesCommand(IReadOnlyDictionary<Vector3, int> newTiles, TilesFactory factory,
            TilesPositionsRepository tilesPositionsRepository)
        {
            _newTiles = newTiles;
            _factory = factory;
            _tilesPositionsRepository = tilesPositionsRepository;
            _oldTiles = _tilesPositionsRepository.Tiles.ToDictionary(pair => pair.Key, pair => pair.Value.GetComponent<EditorTileId>().ID);
        }
        
        public void Execute()
        {
            if (IsExecuted)
                return;

            Clear();
            if (_newTiles.Count > 0)
                Generate(_newTiles);

            IsExecuted = true;
            OnExecuted?.Invoke(this);
        }

        public void Undo()
        {
            if (!IsExecuted)
                return;

            Clear();
            if (_oldTiles.Count > 0)
                Generate(_oldTiles);
            
            IsExecuted = false;
        }

        private void Clear()
        {
            var pos = _tilesPositionsRepository.Positions.ToList();
            foreach (var position in pos)
            {
                var tile = _tilesPositionsRepository.Get(position, true);
                Object.Destroy(tile.gameObject);
            }
        }

        private void Generate(IReadOnlyDictionary<GridKey3, int> tiles)
        {
            foreach (var tileData in tiles)
            {
                var tile = _factory.Create(tileData.Value, tileData.Key);
                tile.gameObject.AddComponent<EditorTileId>().Initialize(tileData.Value);
                _tilesPositionsRepository.Add(tileData.Key, tile);
            }
        }

        private void Generate(IReadOnlyDictionary<Vector3, int> tiles)
        {
            foreach (var tileData in tiles)
            {
                var tile = _factory.Create(tileData.Value, tileData.Key);
                tile.gameObject.AddComponent<EditorTileId>().Initialize(tileData.Value);
                _tilesPositionsRepository.Add(tileData.Key, tile);
            }
        }
    }
}