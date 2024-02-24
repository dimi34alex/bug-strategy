using Unit.Ants.Configs.Professions;
using UnityEngine;

namespace Unit.Ants.Configs
{
    [CreateAssetMenu(fileName = nameof(AntUnitConfig), menuName = "Configs/Units/Ants/" + nameof(AntUnitConfig))]
    public class AntUnitConfig : ScriptableObject
    {
        [field: SerializeField] public float HealthPoints { get; private set; }
        [field: SerializeField] public AntProfessionConfigBase DefaultProfession { get; private set; }
    }
}