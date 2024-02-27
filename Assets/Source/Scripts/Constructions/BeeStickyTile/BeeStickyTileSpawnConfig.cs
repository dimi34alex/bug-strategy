using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "StickyTileConfig", menuName = "Config/StickyTileConfig")]
    public class BeeStickyTileSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeStickyTile> _configuration;

        public ConstructionSpawnConfiguration<BeeStickyTile> GetConfiguration()
        {
            return _configuration;
        }
    }
}