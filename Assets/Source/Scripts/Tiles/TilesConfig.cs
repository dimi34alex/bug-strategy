using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Factories;
using UnityEngine;

namespace BugStrategy.Tiles
{
    [CreateAssetMenu(fileName = nameof(TilesConfig), menuName = "Configs/" + nameof(TilesConfig))]
    public class TilesConfig : ScriptableObject, IFactoryConfig<int, Tile>, ISingleConfig
    {
        [SerializeField] private List<Tile> tiles;

        public IReadOnlyList<Tile> Tiles => tiles;
        private Dictionary<int, Tile> _tiles = null;

        /// <summary>
        /// Result cached
        /// </summary>
        public IReadOnlyDictionary<int, Tile> GetData()
        {
            if (_tiles != null)
                return _tiles;
            
            _tiles = new Dictionary<int, Tile>(tiles.Count);
            for (int i = 0; i < tiles.Count; i++) 
                _tiles.Add(i, tiles[i]);

            return _tiles;
        }
    }
}