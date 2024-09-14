using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy
{
    [CreateAssetMenu(fileName =nameof(StickConfig), menuName = "Configs/StickConfig")]
    public class StickConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public float TakeDamageScale { get; private set; }
    }
}