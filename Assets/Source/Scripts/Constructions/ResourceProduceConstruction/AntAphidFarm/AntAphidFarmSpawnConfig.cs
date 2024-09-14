using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.AntAphidFarm
{
    [CreateAssetMenu(fileName = nameof(AntAphidFarmSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntAphidFarmSpawnConfig))]
    public class AntAphidFarmSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntAphidFarm> Configuration { get; private set; }
    }
}