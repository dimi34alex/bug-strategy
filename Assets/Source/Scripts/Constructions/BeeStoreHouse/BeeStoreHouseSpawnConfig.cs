using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "BeeStoreHouseConfig", menuName = "Config/BeeStoreHouseConfig")]
    public class BeeStoreHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeStoreHouse> _configuration;

        public ConstructionSpawnConfiguration<BeeStoreHouse> GetConfiguration()
        {
            return _configuration;
        }
    }
}