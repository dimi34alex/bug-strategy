using System;
using BugStrategy.CommandsCore;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.MissionEditor.Commands
{
    public abstract class DeleteCommandBase<TId, TTile> : ICommand
        where TTile : MonoBehaviour
    {
        public bool IsExecuted { get; private set; }

        private readonly GridKey3 _point;
        private readonly TId _tileId;
        private readonly GridRepository<TTile> _positionsRepository;
        
        public event Action<ICommand> OnExecuted;

        protected DeleteCommandBase(GridKey3 point, GridRepository<TTile> positionsRepository)
        {
            _point = point;
            _positionsRepository = positionsRepository;
            _tileId = GetId(_positionsRepository.Get(point));
        }
        
        public void Execute()
        {
            if (IsExecuted)
                return;

            var tile = _positionsRepository.Get(_point, true);
            Object.Destroy(tile.gameObject);
            
            IsExecuted = true;
            OnExecuted?.Invoke(this);
        }

        public void Undo()
        {
            if (!IsExecuted)
                return;

            var tile = Create(_tileId, _point);
            _positionsRepository.Add(_point, tile);
            
            IsExecuted = false;
        }
        
        protected abstract TTile Create(TId tileId, Vector3 point);

        protected abstract TId GetId(TTile tile);
    }
}