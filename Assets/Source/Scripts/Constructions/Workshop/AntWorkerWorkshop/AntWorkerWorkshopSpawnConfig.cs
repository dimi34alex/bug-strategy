using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntWorkerWorkshop
{
    [CreateAssetMenu(fileName = nameof(AntWorkerWorkshopSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntWorkerWorkshopSpawnConfig))]
    public class AntWorkerWorkshopSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntWorkerWorkshop> Configuration { get; private set; }
    }
}