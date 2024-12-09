using UnityEngine;

namespace BugStrategy.Unit.Butterflies
{
    [CreateAssetMenu(fileName = nameof(CaterpillarLevel2Config), menuName = "Configs/Units/Butterflies/" + nameof(CaterpillarLevel2Config))]
    public class CaterpillarLevel2Config : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}