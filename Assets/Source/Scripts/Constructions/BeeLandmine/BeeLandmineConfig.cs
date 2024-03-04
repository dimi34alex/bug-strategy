using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeLandmineConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeLandmineConfig))]
    public class BeeLandmineConfig : ConstructionBuyCostConfig
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float ResponseDelay { get; private set; }
        [field: SerializeField] public float StickyTileEffectPower { get; private set; }
        [field: SerializeField] public float StickyTileEffectTime { get; private set; }
    }
}