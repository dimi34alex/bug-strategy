using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.Missions.MissionEditor.Saving;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions
{
    public static class MapLoader
    {
        private const int TaskDelay = 5;

        public static async Task LoadMap(CancellationToken cancellationToken, GridConfig gridConfig, 
            MissionConfig missionConfig, TilesFactory tilesFactory, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourcesRepository resourceSourcesRepository,
            IConstructionFactory constructionFactory, ConstructionsRepository constructionsRepository)
        {
            var missionSave = JsonUtility.FromJson<Mission>(missionConfig.MissionJson.text);

            await LoadGroundTiles(cancellationToken, missionSave.GroundTiles, tilesFactory);
            await LoadConstructionTiles(cancellationToken, missionSave.Constructions, constructionFactory, constructionsRepository);
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
            }
        }

        private static async Task LoadConstructionTiles(CancellationToken cancellationToken,
            IReadOnlyList<Mission.ConstructionPair> tiles, IConstructionFactory factory, ConstructionsRepository constructionsRepository)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                var tile = factory.Create<ConstructionBase>(tiles[i].Id, tiles[i].Affiliation);
                tile.transform.position = tiles[i].Position;
                constructionsRepository.AddConstruction(tiles[i].Position, tile);
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
    }
}