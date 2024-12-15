using UnityEngine;

namespace BugStrategy.Unit.Butterflies
{
    [CreateAssetMenu(fileName = nameof(CaterpillarConfig), menuName = "Configs/Units/Butterflies/" + nameof(CaterpillarConfig))]
    public class CaterpillarConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}