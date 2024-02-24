using UnityEngine;

namespace Unit.Bees.Configs
{
    [CreateAssetMenu(fileName = nameof(BeeMeleeWarriorConfig), menuName = "Configs/Units/Bees/" + nameof(BeeMeleeWarriorConfig))]
    public class BeeMeleeWarriorConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public float InteractionRange { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
    }
}