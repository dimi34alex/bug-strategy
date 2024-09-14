using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(BumblebeeConfig), menuName = "Configs/Units/Bees/" + nameof(BumblebeeConfig))]
    public class BumblebeeConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: Space]
        [field: Header("Ability: Accumulation")]
        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public float ExplosionDamage { get; private set; }
        [field: SerializeField] public LayerMask ExplosionLayers { get; private set; }
    }
}