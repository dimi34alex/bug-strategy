using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public interface IBuilderCell
    {
        public void Activate(int index);
        public void DeActivate();
        public void ManualUpdate();
    }
    
    public class BuilderCell<T> : IBuilderCell where T : MonoBehaviour
    {
        private readonly IReadOnlyList<T> _config;
        private readonly GridConfig _gridConfig;
        private readonly GridRepository<T> _gridRepository;

        private bool _isActive;
        private T _activePrefab;
        private T _activeTile;
        
        public BuilderCell(GridConfig gridConfig, IReadOnlyList<T> config, GridRepository<T> gridRepository)
        {
            _gridConfig = gridConfig;
            _config = config;
            _gridRepository = gridRepository;
        }
        
        private void PrepareTile(int index)
        {
            if (_activeTile != null) 
                Object.Destroy(_activeTile.gameObject);

            _activePrefab = _config[index];
            _activeTile = Object.Instantiate(_activePrefab);
        }

        public void Activate(int index)
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
            => Upd();

        private static bool MouseCursorOverUI() 
            => EventSystem.current.IsPointerOverGameObject();
        
        private void Upd()
        {
            if (!_isActive || _activeTile == null)
                return;
         
            if (Input.GetButtonDown("Fire2"))
                Object.Destroy(_activeTile.gameObject);
            
            if (MouseCursorOverUI())
                return;

            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.y = 0;
            
            _activeTile.transform.position = _gridConfig.RoundPositionToGrid(worldPoint);

            if (Input.GetButtonDown("Fire1"))
            {
                if (_activeTile != null)
                {
                    if (_gridRepository.TryAdd(_activeTile.transform.position, _activeTile))
                    {
                        _activeTile = Object.Instantiate(_activePrefab);
                    }
                }
                else
                    _activeTile = Object.Instantiate(_activePrefab);
            }
        }

        public void Clear()
        {
            var pos = _gridRepository.Positions;
            foreach (var position in pos) 
                _gridRepository.Get(position, true);
        }

        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _config.Count);
            Object.Instantiate(_config[randomIndex], _gridConfig.RoundPositionToGrid(point), quaternion.identity);
        }
    }
}