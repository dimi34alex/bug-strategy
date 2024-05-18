using UnityEngine;

namespace Unit.Effects.Configs
{
    [CreateAssetMenu(fileName = nameof(AttackCooldownDecreaseConfig), menuName = "Configs/Effects/" + nameof(AttackCooldownDecreaseConfig))]
    public class AttackCooldownDecreaseConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float PowerInPercentage { get; private set; }
        [field: SerializeField] public float ExistTime { get; private set; }
    }
}