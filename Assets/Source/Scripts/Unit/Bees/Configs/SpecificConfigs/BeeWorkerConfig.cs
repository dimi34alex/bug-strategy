using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(BeeWorkerConfig), menuName = "Configs/Units/Bees/" + nameof(BeeWorkerConfig))]
    public class BeeWorkerConfig : BeeConfigBase
    {
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}