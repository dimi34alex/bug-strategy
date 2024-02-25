using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "BeesWaxProduceConstructionConfig",
        menuName = "Config/BeesWaxProduceConstructionConfig")]
    public class BeesWaxProduceConstructionSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeesWaxProduceConstruction> _configuration;

        public ConstructionSpawnConfiguration<BeesWaxProduceConstruction> GetConfiguration()
        {
            return _configuration;
        }
    }
}