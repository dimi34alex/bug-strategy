using UnityEngine;

namespace Source.Scripts
{
    [CreateAssetMenu(fileName =nameof(StickConfig), menuName = "Configs/StickConfig")]
    public class StickConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float TakeDamageScale { get; private set; }
    }
}