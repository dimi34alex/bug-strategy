using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BeeStorage
{
    [CreateAssetMenu(fileName = nameof(BeeStorageSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeStorageSpawnConfig))]
    public class BeeStorageSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<BeeStorage> Configuration { get; private set; }
    }
}