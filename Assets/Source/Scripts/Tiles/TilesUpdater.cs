using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class TilesUpdater : CycleInitializerBase
    {
        [Inject] private TilesRepository _tilesRepository;
        
        protected override void OnUpdate()
        {
            foreach (var tile in _tilesRepository.Tiles) 
                tile.HandleUpdate(Time.deltaTime);
        }
    }
}