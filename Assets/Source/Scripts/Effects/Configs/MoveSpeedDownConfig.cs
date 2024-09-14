using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Effects
{
    [CreateAssetMenu(fileName = nameof(MoveSpeedDownConfig), menuName = "Configs/Effects/" + nameof(MoveSpeedDownConfig))]
    public sealed class MoveSpeedDownConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float PowerInPercentage { get; private set; }
        [field: SerializeField] public float ExistTime { get; private set; }
    }
}