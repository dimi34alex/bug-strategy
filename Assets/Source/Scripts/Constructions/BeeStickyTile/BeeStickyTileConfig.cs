using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeStickyTileConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeStickyTileConfig))]
    public class BeeStickyTileConfig : ConstructionBuyCostConfig
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
    }
}