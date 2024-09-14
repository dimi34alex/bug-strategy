using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntRangeWorkshop
{
    [CreateAssetMenu(fileName = nameof(AntRangeWorkshopSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntRangeWorkshopSpawnConfig))]
    public class AntRangeWorkshopSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntRangeWorkshop> Configuration { get; private set; }
    }
}