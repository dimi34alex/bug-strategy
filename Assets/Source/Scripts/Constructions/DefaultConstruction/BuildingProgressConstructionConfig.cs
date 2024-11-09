using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.DefaultConstruction
{
    [CreateAssetMenu(fileName = "BuildingProgressConstructionConfig", menuName = "Configs/Constructions/Main/BuildingProgressConstructionConfig")]
    public class BuildingProgressConstructionConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BuildingProgressConstruction> _configuration;

        public ConstructionSpawnConfiguration<BuildingProgressConstruction> GetConfiguration()
        {
            return _configuration;
        }
    }
}