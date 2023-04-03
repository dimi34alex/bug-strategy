using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private Vector3 centralPosition;
    [SerializeField] private float height;
    [SerializeField] private float width;
    private BuildingGridConfig _constructionConfig;

    [SerializeField] private List<GameObject> tilesPrefabs;
    private Vector3 _currentTilePosition;
    
    void Start()
    {
        _constructionConfig = ConfigsRepository.FindConfig<BuildingGridConfig>() ??
                              throw new NullReferenceException();
        
        _currentTilePosition = centralPosition;
        _currentTilePosition.x -= width/2;
        _currentTilePosition.z += height/2;
        _currentTilePosition =
            FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(_currentTilePosition);
        _currentTilePosition.z +=  _constructionConfig.HexagonsOffcets.y/2;
        
        GenerateMap();
    }

    private void GenerateMap()
    {
        bool stopGenerate = false;
        while (!stopGenerate)
        {
            int tileNum = (int)Random.Range(0, tilesPrefabs.Count);

            Instantiate(tilesPrefabs[tileNum], FrameworkCommander.GlobalData.ConstructionsRepository.RoundPositionToGrid(_currentTilePosition), Quaternion.Euler(0, 0, 0));

            _currentTilePosition.x += _constructionConfig.HexagonsOffcets.x/2;

            if (_currentTilePosition.x > centralPosition.x + width/2)
            {
                _currentTilePosition.x = centralPosition.x - width/2;
                _currentTilePosition.z -= _constructionConfig.HexagonsOffcets.y * 2;
                if (_currentTilePosition.z < centralPosition.z - height/2)
                {
                    stopGenerate = true;
                }
            }  
        }
        
        Destroy(this);
    }
    
}
