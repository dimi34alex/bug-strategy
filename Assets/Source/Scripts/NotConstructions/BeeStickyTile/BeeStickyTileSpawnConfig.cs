using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.NotConstructions.BeeStickyTile
{
    [CreateAssetMenu(fileName = nameof(BeeStickyTileSpawnConfig), menuName = "Configs/NotConstructions/SpawnConfigs/" + nameof(BeeStickyTileSpawnConfig))]
    public class BeeStickyTileSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private NotConstructionSpawnConfiguration<BeeStickyTile> _configuration;

        public NotConstructionSpawnConfiguration<BeeStickyTile> GetConfiguration()
        {
            return _configuration;
        }
    }
}