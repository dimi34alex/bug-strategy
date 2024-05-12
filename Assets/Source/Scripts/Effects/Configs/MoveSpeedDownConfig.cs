using UnityEngine;

namespace Unit.Effects.Configs
{
    [CreateAssetMenu(fileName = nameof(MoveSpeedDownConfig), menuName = "Configs/Effects/" + nameof(MoveSpeedDownConfig))]
    public sealed class MoveSpeedDownConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float PowerInPercentage { get; private set; }
        [field: SerializeField] public float ExistTime { get; private set; }
    }
}