using System.Linq;
using BugStrategy.CommandsCore;
using BugStrategy.Factory;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.MissionEditor
{
    public abstract class GridBuilder<TId, TTile> : IGridBuilder
        where  TTile : MonoBehaviour
    {
        protected readonly GridConfig GridConfig;
        protected readonly GridRepository<TTile> GridRepository;

        private bool _isActive;
        private TId _activeId;
        private TTile _movableModel;

        protected GridBuilder(GridConfig gridConfig, GridRepository<TTile> gridRepository)
        {
            GridConfig = gridConfig;
            GridRepository = gridRepository;
        }
        
        public void Activate(TId index)
        {
            _isActive = true;
            PrepareTile(index);
        }

        public void DeActivate()
        {
            _isActive = false;
            Object.Destroy(_movableModel);
        }

        public void ManualUpdate() 
            => Update();
        
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

        protected abstract TTile CreateMovableModel(TId id, Vector3 point = default);

        private void PrepareTile(TId id)
        {
            if (_movableModel != null) 
                Object.Destroy(_movableModel.gameObject);

            _activeId = id;
            _movableModel = CreateMovableModel(_activeId);
        }
        
        private void Update()
        {
            if (!_isActive || _movableModel == null)
                return;
         
            if (Input.GetButtonDown("Fire2"))
                Object.Destroy(_movableModel.gameObject);
            
            if (MouseCursorOverUI())
                return;

            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.y = 0;   
            
            _movableModel.transform.position = GridConfig.RoundPositionToGrid(worldPoint);

            if (Input.GetButtonDown("Fire1"))
            {
                if (_movableModel != null)
                {
                    if (GridRepository.FreeInExternalGrids(_movableModel.transform.position))
                    {
                        var command = CreateBuildCommand(_activeId, _movableModel.transform.position);
                        command.Execute();
                    }
                }
                else
                    _movableModel = CreateMovableModel(_activeId);
            }
        }
        
        private static bool MouseCursorOverUI() 
            => EventSystem.current.IsPointerOverGameObject();
    }
}