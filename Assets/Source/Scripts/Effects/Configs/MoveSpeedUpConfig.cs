using UnityEngine;

namespace Unit.Effects.Configs
{
    [CreateAssetMenu(fileName = nameof(MoveSpeedUpConfig), menuName = "Configs/Effects/" + nameof(MoveSpeedUpConfig))]
    public sealed class MoveSpeedUpConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float PowerInPercentage { get; private set; }
        [field: SerializeField] public float ExistTime { get; private set; }
    }
}