using BugStrategy.Missions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace BugStrategy
{
    public class MapGeneration : MonoBehaviour
    {
        [SerializeField] private Vector3 centralPosition;
        [SerializeField] private float height;
        [SerializeField] private float width;
        [SerializeField] private int flowerGenChance;

        [Inject] private MissionData _missionData;
        [Inject] private GridConfig _constructionConfig;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;

        private Vector3 _currentTilePosition;

        private void Start()
        {
            _currentTilePosition = centralPosition;
            _currentTilePosition.x -= width/2;
            _currentTilePosition.z += height/2;
            _currentTilePosition = _constructionConfig.RoundPositionToGrid(_currentTilePosition);
            _currentTilePosition.z +=  _constructionConfig.HexagonsOffsets.y/2;
        
            GenerateMap();
        }

        private void GenerateMap()
        {
            while (true)
            {
                var tilePosition = _constructionConfig.RoundPositionToGrid(_currentTilePosition);
                _tilesFactory.Create(tilePosition, Quaternion.identity);

                var tryToSpawnFlower = Random.Range(0, 100);
                if (tryToSpawnFlower < flowerGenChance)
                {
                    var flowerPosition = _constructionConfig.RoundPositionToGrid(_currentTilePosition);
                    var flower = _resourceSourceFactory.Create(flowerPosition, Quaternion.identity);
                    
                    _missionData.ResourceSourcesRepository.Add(flowerPosition, flower);
                    _missionData.ConstructionsRepository.BlockCell(flowerPosition);
                }

                _currentTilePosition.x += _constructionConfig.HexagonsOffsets.x/2;

                if (_currentTilePosition.x > centralPosition.x + width/2)
                {
                    _currentTilePosition.x = centralPosition.x - width/2;
                    _currentTilePosition.z -= _constructionConfig.HexagonsOffsets.y * 2;
                    if (_currentTilePosition.z < centralPosition.z - height/2) 
                        break;
                }  
            }
        }
    }
}
