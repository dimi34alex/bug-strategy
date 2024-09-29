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

        private readonly GroundBuilder _editorGroundGenerator;
        private readonly IReadOnlyDictionary<Vector3, int> _newTiles;
        private readonly IReadOnlyDictionary<GridKey3, int> _oldTiles;
        
        public event Action<ICommand> OnExecuted;
        
        public GenerateGroundTilesCommand(GroundBuilder editorGroundGenerator, 
            IReadOnlyDictionary<Vector3, int> newTiles, IReadOnlyDictionary<GridKey3, int> oldTiles)
        {
            _editorGroundGenerator = editorGroundGenerator;
            _newTiles = newTiles;
            _oldTiles = oldTiles.ToDictionary(k => k.Key, k => k.Value);
        }
        
        public void Execute()
        {
            if (IsExecuted)
                return;

            _editorGroundGenerator.Generate(_newTiles);

            IsExecuted = true;
            OnExecuted?.Invoke(this);
        }

        public void Undo()
        {
            if (!IsExecuted)
                return;

            _editorGroundGenerator.Generate(_oldTiles);
            
            IsExecuted = false;
        }
    }
}