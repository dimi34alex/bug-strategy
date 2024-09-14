using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntUnitConfig), menuName = "Configs/Units/Ants/" + nameof(AntUnitConfig))]
    public class AntUnitConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public AntProfessionConfigBase DefaultProfession { get; private set; }
    }
}