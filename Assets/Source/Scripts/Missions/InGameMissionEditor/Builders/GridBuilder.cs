using System.Linq;
using BugStrategy.Factory;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public abstract class GridBuilder<TId, TTile> : IGridBuilder
        where  TTile : MonoBehaviour
    {
        protected readonly GridConfig GridConfig;
        protected readonly GridRepository<TTile> GridRepository;
        protected readonly ObjectsFactoryBase<TId, TTile> Factory;

        private bool _isActive;
        private TId _activeId;
        private TTile _activeTile;

        protected GridBuilder(GridConfig gridConfig, GridRepository<TTile> gridRepository, 
            ObjectsFactoryBase<TId, TTile> factory)
        {
            GridConfig = gridConfig;
            GridRepository = gridRepository;
            Factory = factory;
        }
        
        public void Activate(TId index)
        {
            _isActive = true;
            PrepareTile(index);
        }

        public void DeActivate()
        {
            _isActive = false;
            Object.Destroy(_activeTile);
        }

        public void ManualUpdate() 
            => Update();

        protected abstract ICommand CreateCommand(TId id, Vector3 point);
        
        protected TTile CreateMovableModel(TId id, Vector3 point = default) 
            => Factory.Create(id, point);

        private void PrepareTile(TId id)
        {
            if (_activeTile != null) 
                Object.Destroy(_activeTile.gameObject);

            _activeId = id;
            _activeTile = CreateMovableModel(_activeId);
        }
        
        private void Update()
        {
            if (!_isActive || _activeTile == null)
                return;
         
            if (Input.GetButtonDown("Fire2"))
                Object.Destroy(_activeTile.gameObject);
            
            if (MouseCursorOverUI())
                return;

            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.y = 0;   
            
            _activeTile.transform.position = GridConfig.RoundPositionToGrid(worldPoint);

            if (Input.GetButtonDown("Fire1"))
            {
                if (_activeTile != null)
                {
                    if (GridRepository.FreeInExternalGrids(_activeTile.transform.position))
                    {
                        var command = CreateCommand(_activeId, _activeTile.transform.position);
                        command.Execute();
                    }
                }
                else
                    _activeTile = CreateMovableModel(_activeId);
            }
        }
        
        public virtual void Clear()
        {
            var pos = GridRepository.Positions.ToList();
            foreach (var position in pos)
            {
                var tile = GridRepository.Get(position, true);
                Object.Destroy(tile.gameObject);
            }
        }
        
        private static bool MouseCursorOverUI() 
            => EventSystem.current.IsPointerOverGameObject();
    }
    
    public interface IGridBuilder
    {
        public void DeActivate();
        public void ManualUpdate();
    }
}