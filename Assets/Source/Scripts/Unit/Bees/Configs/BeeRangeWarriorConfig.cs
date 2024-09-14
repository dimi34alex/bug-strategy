using BugStrategy.Projectiles;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(BeeRangeWarriorConfig), menuName = "Configs/Units/Bees/" + nameof(BeeRangeWarriorConfig))]
    public class BeeRangeWarriorConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
    }
}