using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Missions.InGameMissionEditor.Saving;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;

namespace BugStrategy.Missions
{
    public static class MapEditorLoader
    {
        private const int TaskDelay = 5;

        public static async Task LoadMap(CancellationToken cancellationToken, Mission missionSave, TilesFactory tilesFactory,
            ResourceSourceFactory resourceSourceFactory, ResourceSourceRepository resourceSourcesRepository)
        {
            await LoadGroundTiles(cancellationToken, missionSave.GroundTiles, tilesFactory);
            await LoadResourceSources(cancellationToken, missionSave.ResourceSources, resourceSourceFactory,
                resourceSourcesRepository);
        }

        private static async Task LoadGroundTiles(CancellationToken cancellationToken, IReadOnlyList<Mission.TilePair> groundTiles, TilesFactory tilesFactory)
        {
            for (int i = 0; i < groundTiles.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                tilesFactory.Create(groundTiles[i].Id, groundTiles[i].Position);
            }
        }

        private static async Task LoadResourceSources(CancellationToken cancellationToken, 
            IReadOnlyList<Mission.ResourceSourcePair> resourceSources, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourceRepository resourceSourcesRepository)
        {
            for (int i = 0; i < resourceSources.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                var flower = resourceSourceFactory.Create(resourceSources[i].Id, resourceSources[i].Position);
                resourceSourcesRepository.Add(resourceSources[i].Position, flower);
            }
        }
    }
}