using System.Collections.Generic;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.ResourceSources;
using UnityEngine;
using ResourceSourceFactory = BugStrategy.ResourceSources.ResourceSourceFactory;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class ResourceSourcesBuilder : GridBuilder<int, ResourceSourceBase>
    {
        private readonly CommandsFactory _commandsFactory;
        private readonly IReadOnlyList<int> _keys;
        
        public ResourceSourcesBuilder(GridConfig gridConfig, GridRepository<ResourceSourceBase> gridRepository, 
            ResourceSourceFactory factory, CommandsFactory commandsFactory) 
            : base(gridConfig, gridRepository, factory)
        {
            _commandsFactory = commandsFactory;
            _keys = factory.GetKeys();
        }
        
        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _keys.Count);
            var id = _keys[randomIndex];
            var tile = CreateMovableModel(id, point);
            
            if (!GridRepository.TryAdd(point, tile)) 
                Debug.LogError($"Cant spawn tile: {point}");
        }

        protected override ICommand CreateCommand(int id, Vector3 point) 
            => _commandsFactory.BuildResourceSourceCommand(id, point);
    }
}