using System;
using BugStrategy.Grids.GridsProviding;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class TilesContentHider : IDisposable
    {
        private readonly TilesRepository _tilesRepository;
        private readonly IGridProvider _gridProvider;

        public TilesContentHider(bool showContentOnBecameTileFree, TilesRepository tilesRepository, IGridProvider gridProvider)
        {
            _tilesRepository = tilesRepository;
            _gridProvider = gridProvider;

            _gridProvider.OnAdd += TryHide;
            if (showContentOnBecameTileFree) 
                _gridProvider.OnRemove += TryShow;
        }

        private void TryHide(Vector3 position)
        {
            if (_tilesRepository.Exist(position))
            {
                var tile = _tilesRepository.Get(position);
                tile.ToggleContentVisibility(false);
            }
        }
        
        private void TryShow(Vector3 position)
        {
            if (_tilesRepository.Exist(position))
            {
                var tile = _tilesRepository.Get(position);
                tile.ToggleContentVisibility(true);
            }
        }

        public void Dispose()
        {
            _gridProvider.OnAdd -= TryHide;
            _gridProvider.OnRemove -= TryShow;
        }
    }
}