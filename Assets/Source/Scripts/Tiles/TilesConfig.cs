using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Tiles
{
    [CreateAssetMenu(fileName = nameof(TilesConfig), menuName = "Configs/" + nameof(TilesConfig))]
    public class TilesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<Tile> tiles;

        public IReadOnlyList<Tile> Tiles => tiles;
    }
}