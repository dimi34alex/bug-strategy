using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.AntMine
{
    [CreateAssetMenu(fileName = nameof(AntMineSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntMineSpawnConfig))]
    public class AntMineSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntMine> Configuration { get; private set; }
    }
}