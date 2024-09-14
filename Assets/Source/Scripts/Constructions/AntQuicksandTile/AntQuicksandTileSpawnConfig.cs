using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntQuicksandTile
{
    [CreateAssetMenu(fileName = nameof(AntQuicksandTileSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntQuicksandTileSpawnConfig))]
    public class AntQuicksandTileSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntQuicksandTile> Configuration { get; private set; }
    }
}