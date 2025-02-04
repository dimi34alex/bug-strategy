using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.DefaultConstruction
{
    [CreateAssetMenu(fileName = nameof(DefaultConstructionSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(DefaultConstructionSpawnConfig))]
    public class DefaultConstructionSpawnConfig : ScriptableObject, ISingleConfig
    { 
        [SerializeField] private ConstructionSpawnConfiguration<DefaultConstruction> _configuration;

        public ConstructionSpawnConfiguration<DefaultConstruction> GetConfiguration() 
            => _configuration;
    }
}
 