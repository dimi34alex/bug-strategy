using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyStorage
{
    [CreateAssetMenu(fileName = nameof(ButterflyStorageSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(ButterflyStorageSpawnConfig))]
    public class ButterflyStorageSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<ButterflyStorage> Configuration { get; private set; }

    }
}