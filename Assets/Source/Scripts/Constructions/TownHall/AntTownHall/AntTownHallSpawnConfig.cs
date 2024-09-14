using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntTownHall
{
    [CreateAssetMenu(fileName = nameof(AntTownHallSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntTownHallSpawnConfig))]
    public class AntTownHallSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntTownHall> Configuration { get; private set; }
    }
}