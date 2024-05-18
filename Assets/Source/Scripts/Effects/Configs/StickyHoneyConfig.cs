using UnityEngine;

namespace Unit.Effects.Configs
{
    [CreateAssetMenu(fileName = nameof(StickyHoneyConfig), menuName = "Configs/Effects/" + nameof(StickyHoneyConfig))]
    public sealed class StickyHoneyConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public int ExistTime { get; private set; }
    }
}