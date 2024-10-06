using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.CommandsCore;
using BugStrategy.Missions.MissionEditor.Commands;
using BugStrategy.Missions.MissionEditor.GridRepositories;
using BugStrategy.Missions.MissionEditor.Saving;
using BugStrategy.ResourceSources;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    public class ResourceSourcesBuilder : GridBuilder<int, ResourceSourceBase>
    {
        private readonly ResourceSourceFactory _factory;
        private readonly MissionEditorCommandsFactory _missionEditorCommandsFactory;
        private readonly IReadOnlyList<int> _keys;
        
        public ResourceSourcesBuilder(GridConfig gridConfig, GridRepository<ResourceSourceBase> gridRepository, 
            ResourceSourceFactory factory, MissionEditorCommandsFactory missionEditorCommandsFactory) 
            : base(gridConfig, gridRepository)
        {
            _factory = factory;
            _missionEditorCommandsFactory = missionEditorCommandsFactory;
            _keys = factory.GetKeys();
        }
        
        protected override ICommand CreateBuildCommand(int id, Vector3 point) 
            => _missionEditorCommandsFactory.BuildResourceSourceCommand(id, point);

        protected override ResourceSourceBase CreateMovableModel(int id, Vector3 point = default) 
            => _factory.Create(id);

        public void ManualRandomSpawn(Vector3 point)
        {
            var randomIndex = Random.Range(0, _keys.Count);
            var id = _keys[randomIndex];
            var tile = CreateMovableModel(id, point);
            
            if (!GridRepository.TryAdd(point, tile)) 
                Debug.LogError($"Cant spawn tile: {point}");
        }
        
        public async Task LoadResourceSources(CancellationToken cancellationToken, 
            IReadOnlyList<Mission.ResourceSourcePair> resourceSources)
        {
            for (int i = 0; i < resourceSources.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(5, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                var resourceSource = _factory.Create(resourceSources[i].Id, resourceSources[i].Position);
                resourceSource.gameObject.AddComponent<MissionEditorTileId>().Initialize(resourceSources[i].Id);

                GridRepository.Add(resourceSources[i].Position, resourceSource);
            }
        }
    }
}