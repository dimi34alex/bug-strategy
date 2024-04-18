using UnityEngine;

namespace Unit.Effects.Configs
{
    [CreateAssetMenu(fileName = nameof(PoisonConfig), menuName = "Configs/Effects/" + nameof(PoisonConfig))]
    public sealed class PoisonConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float DamagePerSecond { get; private set; }
        [field: SerializeField] public int ExistTime { get; private set; }
    }
}