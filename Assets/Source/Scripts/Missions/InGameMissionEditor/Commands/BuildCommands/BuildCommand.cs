using System;
using BugStrategy.Factory;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public abstract class BuildCommand<TId, TResult> : ICommand
        where TResult : MonoBehaviour
    {
        public bool IsExecuted { get; private set; }

        private TId _tileIdReplaced;
        private readonly TId _defaultId;
        private readonly TId _tileId;
        private readonly Vector3 _point;
        private readonly ObjectsFactoryBase<TId, TResult> _factory;
        private readonly GridRepository<TResult> _positionsRepository;

        public event Action<ICommand> OnExecuted;
        
        protected BuildCommand(TId defaultId, TId tileId, Vector3 point, ObjectsFactoryBase<TId, TResult> factory, 
            GridRepository<TResult> positionsRepository)
        {
            _tileIdReplaced = _defaultId = defaultId;
            _tileId = tileId;
            _point = point;
            _factory = factory;
            _positionsRepository = positionsRepository;
        }

        public void Execute()
        {
            if (IsExecuted)
                return;

            if (_positionsRepository.Exist(_point, true, false))
            {
                var oldTile = _positionsRepository.Get(_point, true);
                _tileIdReplaced = GetId(oldTile);
                Object.Destroy(oldTile.gameObject);
            }

            var newTile = Create(_tileId, _point);
            if (!_positionsRepository.TryAdd(_point, newTile))
                throw new Exception("Tile cant be placed");

            IsExecuted = true;
            OnExecuted?.Invoke(this);
        }

        public void Undo()
        {
            if (!IsExecuted)
                return;

            if (_positionsRepository.Exist(_point, true, false))
            {
                var newTile = _positionsRepository.Get(_point, true);
                Object.Destroy(newTile.gameObject);
            }

            if (ReplacedIdIsValid(_tileIdReplaced))
            {
                var oldTile = Create(_tileIdReplaced, _point);
                if (!_positionsRepository.TryAdd(_point, oldTile))
                    throw new Exception("Tile cant be placed");
            
                _tileIdReplaced = _defaultId;
            }

            IsExecuted = false;
        }

        protected virtual TResult Create(TId id, Vector3 point) 
            => _factory.Create(id, point);

        protected abstract TId GetId(TResult tile);

        protected abstract bool ReplacedIdIsValid(TId replacedId);
    }
}