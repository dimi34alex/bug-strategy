using System.Linq;
using BugStrategy.CommandsCore;
using BugStrategy.Grids;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.MissionEditor
{
    public abstract class GridBuilder<TId, TTile> : IGridBuilder
        where  TTile : MonoBehaviour
    {
        protected readonly GridConfig GridConfig;
        protected readonly GridRepository<TTile> GridRepository;

        public bool IsActive { get; private set; }
        
        private TId _activeId;
        private TTile _movableModel;

        protected GridBuilder(GridConfig gridConfig, GridRepository<TTile> gridRepository)
        {
            GridConfig = gridConfig;
            GridRepository = gridRepository;
        }
        
        public void Activate(TId index, Vector3 position)
        {
            IsActive = true;
            PrepareTile(index, position);
        }

        public void DeActivate()
        {
            if (!IsActive)
                return;

            IsActive = false;

            if (_movableModel != null)
                Object.Destroy(_movableModel.gameObject);
        }

        public void ApplyBuild()
        {
            if (_movableModel == null) 
                return;
           
            if (GridRepository.FreeInExternalGrids(_movableModel.transform.position))
            {
                var command = CreateBuildCommand(_activeId, _movableModel.transform.position);
                command.Execute();
            }
        }
        
        public void Move(Vector3 point)
        {
            if (!IsActive || _movableModel == null)
                return;
            
            _movableModel.transform.position = GridConfig.RoundPositionToGrid(point);
        }

        public bool Clear(Vector3 point)
        {
            point = GridConfig.RoundPositionToGrid(point);
            if (GridRepository.Exist(point))
            {
                var command = CreateDeleteCommand(point);
                command.Execute();
                return true;
            }

            return false;
        }

        public void Clear()
        {
            var pos = GridRepository.Positions.ToList();
            foreach (var position in pos)
            {
                var tile = GridRepository.Get(position, true);
                Object.Destroy(tile.gameObject);
            }
        }

        protected abstract ICommand CreateBuildCommand(TId id, Vector3 point);
        protected abstract ICommand CreateDeleteCommand(GridKey3 point);

        protected abstract TTile CreateMovableModel(TId id, Vector3 position);

        private void PrepareTile(TId id, Vector3 position)
        {
            if (_movableModel != null) 
                Object.Destroy(_movableModel.gameObject);

            _activeId = id;
            _movableModel = CreateMovableModel(_activeId, position);
        }
    }
}