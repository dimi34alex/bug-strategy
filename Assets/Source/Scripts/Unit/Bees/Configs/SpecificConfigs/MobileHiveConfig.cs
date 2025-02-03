using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(MobileHiveConfig), menuName = "Configs/Units/Bees/" + nameof(MobileHiveConfig))]
    public class MobileHiveConfig : BeeConfigBase
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: Space]
        [field: Header("Ability: armor breakthrough")]
        [field: SerializeField] public float ExplosionDamage { get; private set; }
        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public LayerMask ExplosionLayers { get; private set; }
        [field: SerializeField] public SerializableDictionary<UnitType, int> spawnUnits;
    }
}