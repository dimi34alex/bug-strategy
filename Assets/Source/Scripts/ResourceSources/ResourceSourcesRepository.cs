using System;
using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.ResourceSources
{
    public class ResourceSourcesRepository
    {
        private readonly GridConfig _constructionConfig;
        private readonly Dictionary<GridKey3, ResourceSourceBase> _resourceSources;
        private readonly HashSet<GridKey3> _blockedCells;

        public event Action<Vector3> OnAdd;
        public event Action<Vector3> OnRemove;

        public ResourceSourcesRepository()
        {
            _constructionConfig = ConfigsRepository.ConfigsRepository.FindConfig<GridConfig>() ??
                                  throw new NullReferenceException();

            _resourceSources = new Dictionary<GridKey3, ResourceSourceBase>();
            _blockedCells = new HashSet<GridKey3>();
        }

        public bool Exist(Vector3 position, bool blockIgnore = true)
        {
            return _resourceSources.ContainsKey(position) || !blockIgnore && _blockedCells.Contains(position);
        }

        public void Add(Vector3 position, ResourceSourceBase resourceSource)
        {
            if (_resourceSources.ContainsKey(position))
                throw new Exception($"Position {position} already exist in grid");

            _resourceSources.Add(position, resourceSource);
            OnAdd?.Invoke(position);
        }
        
        public ResourceSourceBase Get(Vector3 position, bool withExtract = false)
        {
            if (!_resourceSources.ContainsKey(position))
                throw new Exception($"Position {position} not found");

            var resourceSource = _resourceSources[position];

            if (withExtract)
            {
                _resourceSources.Remove(position);
                OnRemove?.Invoke(position);
            }

            return resourceSource;
        }
    
        public ResourceSourceBase GetNearest(Vector3 position, bool onlyCanBeCollected)
        {
            if (_resourceSources.Count <= 0)
                throw new ArgumentException();
        
            var currentDistance = float.MaxValue;
            ResourceSourceBase nearestResourceSource = null;
            foreach (var resourceSource in _resourceSources)
            {
                if(onlyCanBeCollected && !resourceSource.Value.CanBeCollected)
                    continue;
            
                var distance = Vector3.Distance(position, resourceSource.Value.transform.position);
                if (distance < currentDistance)
                {
                    currentDistance = distance;
                    nearestResourceSource = resourceSource.Value;
                }
            }
        
            if (nearestResourceSource == null)
                throw new NullReferenceException($"nearestResourceSource is null");
        
            return nearestResourceSource;
        }
    
        public void BlockCell(Vector3 position)
        {
            _blockedCells.Add(position);
        }

        public void UnblockCell(Vector3 position)
        {
            _blockedCells.Remove(position);
        }

        public bool CellIsBlocked(Vector3 position)
        {
            return _blockedCells.Contains(position);
        }
    }
}