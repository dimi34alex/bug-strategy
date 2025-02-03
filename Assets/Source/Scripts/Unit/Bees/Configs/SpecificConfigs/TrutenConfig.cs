using UnityEngine;

namespace BugStrategy.Unit.Bees
{
    [CreateAssetMenu(fileName = nameof(TrutenConfig), menuName = "Configs/Units/Bees/" + nameof(TrutenConfig))]
    public class TrutenConfig : BeeConfigBase
    {
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }   
        [field: Space]
        [field: Header("Ability: Standard Bearer")]
        [field: SerializeField] public float StandardBearerRadius { get; private set; }
        [field: Space]
        [field: Header("Ability: Brave Death")]
        [field: SerializeField] public float HealValue { get; private set; }
        [field: SerializeField] public float HealRadius { get; private set; }
        [field: SerializeField] public LayerMask HealLayers { get; private set; }

    }
}