using System.Collections.Generic;
using System.Linq;
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
        protected readonly GridRepository< TTile> GridRepository;

        private bool _isActive;
        private TId _activeId;
        private TTile _activeTile;

        protected GridBuilder(GridConfig gridConfig, GridRepository< TTile> gridRepository)
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
            Object.Destroy(_activeTile);
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

        protected abstract TTile Create(TId id, Vector3 point = default);
        
        private void PrepareTile(TId id)
        {
            if (_activeTile != null) 
                Object.Destroy(_activeTile.gameObject);

            _activeId = id;
            _activeTile = Create(_activeId);
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
                    if (GridRepository.TryAdd(_activeTile.transform.position, _activeTile))
                        _activeTile = Create(_activeId);
                }
                else
                    _activeTile = Create(_activeId);
            }
        }
        
        private static bool MouseCursorOverUI() 
            => EventSystem.current.IsPointerOverGameObject();
    }
    
    public interface IGridBuilder
    {
        public void DeActivate();
        public void ManualUpdate();
        public void Clear();
    }
}