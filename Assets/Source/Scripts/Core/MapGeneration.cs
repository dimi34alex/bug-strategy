using System.Collections.Generic;
using BugStrategy.Missions;
using BugStrategy.ResourceSources;
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

        [SerializeField] private List<GameObject> tilesPrefabs;

        [SerializeField] private List<ResourceSourceBase> flowerPrefabs;
    
        [SerializeField] private GameObject bushPrefab;
        [SerializeField] private GameObject grassPrefab;
        [SerializeField] private GameObject cloverPrefab;

        [Inject] private MissionData _missionData;
        [Inject] private GridConfig _constructionConfig;

        private Vector3 _currentTilePosition;

        public int flowerGenChance;
        public int bushGenChance;
        public int grassGenChance;
        public int cloverGenChance;

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
            bool stopGenerate = false;
            while (!stopGenerate)
            {
                int tileNum = (int)Random.Range(0, tilesPrefabs.Count);

                Instantiate(tilesPrefabs[tileNum], _constructionConfig.RoundPositionToGrid(_currentTilePosition), Quaternion.Euler(0, 0, 0), this.transform);

                int tryToSpawnFlower = (int)Random.Range(0, 100);
                int tryToSpawnBush = (int)Random.Range(0, 100);
                int tryToSpawnGrass = (int)Random.Range(0, 100);
                int tryToSpawnClover = (int)Random.Range(0, 100);

                if (tryToSpawnFlower < flowerGenChance)
                {
                    int flowerNum = (int)Random.Range(0, flowerPrefabs.Count);

                    var flowerPosition = _constructionConfig.RoundPositionToGrid(_currentTilePosition);
                    var flower = Instantiate(flowerPrefabs[flowerNum], flowerPosition, Quaternion.Euler(0, 0, 0), this.transform);
                    _missionData.ResourceSourcesRepository.Add(flowerPosition, flower);
                    _missionData.ConstructionsRepository.BlockCell(flowerPosition);
                }
                else if (tryToSpawnBush < bushGenChance)
                {
                    Instantiate(bushPrefab, _constructionConfig.RoundPositionToGrid(_currentTilePosition), Quaternion.Euler(0, 0, 0), this.transform);
                }
                else if (tryToSpawnGrass < grassGenChance)
                {
                    Instantiate(grassPrefab, _constructionConfig.RoundPositionToGrid(_currentTilePosition), Quaternion.Euler(0, 0, 0), this.transform);
                }
                else if (tryToSpawnClover < cloverGenChance)
                {
                    Instantiate(cloverPrefab, _constructionConfig.RoundPositionToGrid(_currentTilePosition), Quaternion.Euler(0, 0, 0), this.transform);
                }


                _currentTilePosition.x += _constructionConfig.HexagonsOffsets.x/2;

                if (_currentTilePosition.x > centralPosition.x + width/2)
                {
                    _currentTilePosition.x = centralPosition.x - width/2;
                    _currentTilePosition.z -= _constructionConfig.HexagonsOffsets.y * 2;
                    if (_currentTilePosition.z < centralPosition.z - height/2)
                    {
                        stopGenerate = true;
                    }
                }  
            }
        }
    }
}
