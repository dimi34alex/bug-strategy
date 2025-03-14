using UnityEngine;

namespace BugStrategy.Constructions.BuildProgressConstructions
{
    [CreateAssetMenu(fileName = nameof(BuildingProgressConstructionConfig), menuName = "Configs/Constructions/Main/" + nameof(BuildingProgressConstructionConfig))]
    public class BuildingProgressConstructionConfig : ConstructionConfigBase
    {
        [field: SerializeField] public int MaxHealthPoints;
    }
}