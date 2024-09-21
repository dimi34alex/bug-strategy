using System.Collections.Generic;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using UnityEngine;
using ResourceSourceFactory = BugStrategy.ResourceSources.ResourceSourceFactory;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class ResourceSourcesBuilder : GridBuilder<int, ResourceSourceBase>
    {
        private readonly IReadOnlyList<int> _keys;
        private readonly ResourceSourceFactory _factory;
        
        public ResourceSourcesBuilder(GridConfig gridConfig, GridRepository<ResourceSourceBase> gridRepository, ResourceSourceFactory factory) 
            : base(gridConfig, gridRepository)
        {
            _factory = factory;
            _keys = _factory.GetKeys();
        }
        
        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _keys.Count);
            var id = _keys[randomIndex];
            var tile = Create(id, point);
            
            if (!GridRepository.TryAdd(point, tile)) 
                Debug.LogError($"Cant spawn tile: {point}");
        }

        protected override ResourceSourceBase Create(int id, Vector3 point = default) 
            => _factory.Create(id, GridConfig.RoundPositionToGrid(point), Quaternion.identity);
    }
}