using UnityEngine;

namespace Unit.Bees.Configs
{
    [CreateAssetMenu(fileName = nameof(BeeWorkerConfig), menuName = "Configs/Units/Bees/" + nameof(BeeWorkerConfig))]
    public class BeeWorkerConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}