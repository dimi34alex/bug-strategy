using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeSiegeWeaponsBarrackSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeSiegeWeaponsBarrackSpawnConfig))]
    public class BeeSiegeWeaponsBarrackSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<BeeSiegeWeaponsBarrack> Configuration { get; private set; }
    }
}