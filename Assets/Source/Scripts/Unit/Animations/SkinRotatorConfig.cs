using UnityEngine;

namespace BugStrategy.Unit.Animations
{
    [CreateAssetMenu(fileName = nameof(SkinRotatorConfig), menuName = "Configs/Units/" + nameof(SkinRotatorConfig))]
    public class SkinRotatorConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 90)] public float AngleClamp { get; private set; }
    }
}