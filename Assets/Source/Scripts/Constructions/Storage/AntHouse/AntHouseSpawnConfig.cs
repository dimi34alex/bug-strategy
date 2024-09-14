using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntHouse
{
    [CreateAssetMenu(fileName = nameof(AntHouseSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntHouseSpawnConfig))]
    public class AntHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntHouse> Configuration { get; private set; }
    }
}