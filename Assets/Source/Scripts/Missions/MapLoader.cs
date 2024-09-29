using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.Missions.InGameMissionEditor.Saving;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BugStrategy.Missions
{
    public static class MapLoader
    {
        private const int FlowerGenChance = 10;
        private const int TaskDelay = 5;

        public static async Task LoadMap(CancellationToken cancellationToken, GridConfig gridConfig, 
            MissionConfig missionConfig, TilesFactory tilesFactory, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourcesRepository resourceSourcesRepository,
            IConstructionFactory constructionFactory, ConstructionsRepository constructionsRepository)
        {
            var missionSave = JsonUtility.FromJson<Mission>(missionConfig.MissionJson.text);

            await LoadGroundTiles(cancellationToken, missionSave.GroundTiles, tilesFactory);
            await LoadResourceSources(cancellationToken, missionSave.ResourceSources, resourceSourceFactory,
                resourceSourcesRepository, constructionsRepository);
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
                // TrySpawnResourceSource(missionSave.GroundTiles[i].Position, gridConfig, resourceSourceFactory,
                //     resourceSourcesRepository, constructionsRepository);
            }
        }

        private static async Task LoadResourceSources(CancellationToken cancellationToken, 
            IReadOnlyList<Mission.ResourceSourcePair> resourceSources, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourcesRepository resourceSourcesRepository, 
            ConstructionsRepository constructionsRepository)
        {
            for (int i = 0; i < resourceSources.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                var flower = resourceSourceFactory.Create(resourceSources[i].Id, resourceSources[i].Position);
                resourceSourcesRepository.Add(resourceSources[i].Position, flower);
                constructionsRepository.BlockCell(resourceSources[i].Position);
            }
        }
        
        private static void TrySpawnResourceSource(Vector3 position, GridConfig gridConfig, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourcesRepository resourceSourcesRepository,
            ConstructionsRepository constructionsRepository)
        {
            var tryToSpawnFlower = Random.Range(0, 100);
            if (tryToSpawnFlower < FlowerGenChance)
            {
                var flowerPosition = gridConfig.RoundPositionToGrid(position);
                var flower = resourceSourceFactory.Create(flowerPosition);
                    
                resourceSourcesRepository.Add(flowerPosition, flower);
                constructionsRepository.BlockCell(flowerPosition);
            }
        }
    }
}