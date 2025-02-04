using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BuildProgressConstructions
{
    [CreateAssetMenu(fileName = "BuildingProgressConstructionConfig", menuName = "Configs/Constructions/Main/BuildingProgressConstructionConfig")]
    public class BuildingProgressConstructionSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BuildingProgressConstruction> _configuration;

        public ConstructionSpawnConfiguration<BuildingProgressConstruction> GetConfiguration()
        {
            return _configuration;
        }
    }
}