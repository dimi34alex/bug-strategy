using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    [CreateAssetMenu(fileName = nameof(TechBeeLandmineDamageConfig), 
        menuName = "Configs/Technologies/" + nameof(TechBeeLandmineDamageConfig))]
    public class TechBeeLandmineDamageConfig : TechnologyConfig
    {
        [field: SerializeField] public float DamageScale { get; private set; }
    }
}