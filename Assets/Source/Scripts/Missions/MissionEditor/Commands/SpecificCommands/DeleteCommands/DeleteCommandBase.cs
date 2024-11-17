using System;
using BugStrategy.CommandsCore;
using BugStrategy.Factories;
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
        private readonly FactoryWithId<TId, TTile> _factory;
        private readonly GridRepository<TTile> _positionsRepository;
        
        public event Action<ICommand> OnExecuted;

        protected DeleteCommandBase(GridKey3 point, FactoryWithId<TId, TTile> factory, 
            GridRepository<TTile> positionsRepository)
        {
            _point = point;
            _factory = factory;
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

            var tile = _factory.Create(_tileId, _point);
            _positionsRepository.Add(_point, tile);
            
            IsExecuted = false;
        }

        protected abstract TId GetId(TTile tile);
    }
}