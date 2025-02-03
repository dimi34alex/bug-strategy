using BugStrategy.Projectiles;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(WaspConfig), menuName = "Configs/Units/Bees/" + nameof(WaspConfig))]
    public class WaspConfig : BeeConfigBase
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }
    }
}