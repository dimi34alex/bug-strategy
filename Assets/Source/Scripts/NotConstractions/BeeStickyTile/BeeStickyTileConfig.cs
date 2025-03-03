using UnityEngine;

namespace BugStrategy.NotConstructions.BeeStickyTile
{
    [CreateAssetMenu(fileName = nameof(BeeStickyTileConfig), menuName = "Configs/NotConstructions/Main/" + nameof(BeeStickyTileConfig))]
    public class BeeStickyTileConfig : NotConstructionBuyCostConfig
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float DelayBeforeApply { get; private set; }
        [field: SerializeField] [field: Range(0, 30)] public float ExistTime { get; private set; }
    }
}