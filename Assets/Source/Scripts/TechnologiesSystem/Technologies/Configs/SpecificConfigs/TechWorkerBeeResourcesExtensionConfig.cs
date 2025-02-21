using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    [CreateAssetMenu(fileName = nameof(TechWorkerBeeResourcesExtensionConfig), 
        menuName = "Configs/Technologies/" + nameof(TechWorkerBeeResourcesExtensionConfig))]
    public class TechWorkerBeeResourcesExtensionConfig : TechnologyConfig
    {
        [field: Space]
        [field: SerializeField] public float ResourcesCapacityScale { get; private set; }
    }
}