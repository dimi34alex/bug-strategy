using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Constructions;
using BugStrategy.Constructions.Factory;
using BugStrategy.Missions.MissionEditor.Saving;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using Unity.Mathematics;
using UnityEngine;

namespace BugStrategy.Missions
{
    public static class MapLoader
    {
        private const int TaskDelay = 5;

        /// <returns> return abs of the most distant tiles, it can be referred as field size </returns>
        public static async Task<Vector2> LoadMap(CancellationToken cancellationToken, MissionConfig missionConfig, 
            TilesFactory tilesFactory, 
            ResourceSourceFactory resourceSourceFactory, ResourceSourcesRepository resourceSourcesRepository,
            IConstructionFactory constructionFactory, ConstructionsRepository constructionsRepository)
        {
            var missionSave = JsonUtility.FromJson<Mission>(missionConfig.MissionJson.text);

            var fieldSize = await LoadGroundTiles(cancellationToken, missionSave.GroundTiles, tilesFactory);
            await LoadConstructionTiles(cancellationToken, missionSave.Constructions, constructionFactory);
            await LoadResourceSources(cancellationToken, missionSave.ResourceSources, resourceSourceFactory,
                resourceSourcesRepository, constructionsRepository);

            return fieldSize;
        }

        private static async Task<Vector2> LoadGroundTiles(CancellationToken cancellationToken, 
            IReadOnlyList<Mission.TilePair> groundTiles, TilesFactory tilesFactory)
        {
            var fieldSize = Vector2.zero;
            for (int i = 0; i < groundTiles.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                tilesFactory.Create(groundTiles[i].Id, groundTiles[i].Position);

                var pos = groundTiles[i].Position.Position;
                
                if (math.abs(pos.x) > fieldSize.x) 
                    fieldSize.x = math.abs(pos.x);
                
                if (math.abs(pos.z) > fieldSize.y) 
                    fieldSize.y = math.abs(pos.z);
            }

            return fieldSize;
        }

        private static async Task LoadConstructionTiles(CancellationToken cancellationToken,
            IReadOnlyList<Mission.ConstructionPair> tiles, IConstructionFactory factory)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (i % 10 == 0) 
                    await Task.Delay(TaskDelay, cancellationToken);
                
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                factory.Create<ConstructionBase>(tiles[i].Id, tiles[i].Position, tiles[i].Affiliation);
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