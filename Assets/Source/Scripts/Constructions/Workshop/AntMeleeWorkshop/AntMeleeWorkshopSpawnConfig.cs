using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.AntMeleeWorkshop
{
    [CreateAssetMenu(fileName = nameof(AntMeleeWorkshopSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(AntMeleeWorkshopSpawnConfig))]
    public class AntMeleeWorkshopSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<AntMeleeWorkshop> Configuration { get; private set; }
    }
}