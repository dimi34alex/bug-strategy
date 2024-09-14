using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyHouse
{
    [CreateAssetMenu(fileName = nameof(ButterflyHouseSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(ButterflyHouseSpawnConfig))]
    public class ButterflyHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<ButterflyHouse> Configuration { get; private set; }
    }
}