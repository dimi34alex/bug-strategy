using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(WorkerBeeConfig), menuName = "Configs/Units/Bees/" + nameof(WorkerBeeConfig))]
    public class WorkerBeeConfig : BeeConfigBase
    {
        [field: Header("Gathering")]
        [field: SerializeField, Min(0)] public int GatheringCapacity { get; private set; }
        [field: SerializeField, Min(0)] public float GatheringTime { get; private set; }
        [field: Header("Repairing")]
        [field: SerializeField, Min(0)] public float RepairValue { get; private set; }
        [field: SerializeField, Min(0)] public float RepairCooldown { get; private set; }
        [field: Header("Hive Protection")]
        [field: SerializeField, Min(0)] public float AttackDamage { get; private set; }
        [field: SerializeField, Min(0)] public float AttackRange { get; private set; }

    }
}