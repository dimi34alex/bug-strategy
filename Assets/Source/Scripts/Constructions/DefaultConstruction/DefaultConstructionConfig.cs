using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.DefaultConstruction
{
    [CreateAssetMenu(fileName = "DefaultConstructionConfig", menuName = "Config/DefaultConstructionConfig")]
    public class DefaultConstructionConfig : ScriptableObject, ISingleConfig
    { 
        [SerializeField] private ConstructionSpawnConfiguration<DefaultConstruction> _configuration;

        public ConstructionSpawnConfiguration<DefaultConstruction> GetConfiguration()
        {
            return _configuration;
        }
    }
}
 