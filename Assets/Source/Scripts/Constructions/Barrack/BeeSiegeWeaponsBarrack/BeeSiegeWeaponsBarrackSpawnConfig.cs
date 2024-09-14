using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BeeSiegeWeaponsBarrack
{
    [CreateAssetMenu(fileName = nameof(BeeSiegeWeaponsBarrackSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeSiegeWeaponsBarrackSpawnConfig))]
    public class BeeSiegeWeaponsBarrackSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<BeeSiegeWeaponsBarrack> Configuration { get; private set; }
    }
}