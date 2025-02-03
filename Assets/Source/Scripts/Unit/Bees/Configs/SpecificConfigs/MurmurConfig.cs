using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(MurmurConfig), menuName = "Configs/Units/Bees/" + nameof(MurmurConfig))]
    public class MurmurConfig : BeeConfigBase
    {
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
    }
}