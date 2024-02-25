using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "AntStoreHouseConfig", menuName = "Config/AntStoreHouseConfig")]
    public class AntStoreHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<AntStoreHouse> _configuration;

        public ConstructionSpawnConfiguration<AntStoreHouse> GetConfiguration()
        {
            return _configuration;
        }
    }
}