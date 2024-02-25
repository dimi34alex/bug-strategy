using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "AntStoreHouseConfig", menuName = "Config/AntStoreHouseConfig")]
    public class ButterflyStoreHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<ButterflyStoreHouse> _configuration;

        public ConstructionSpawnConfiguration<ButterflyStoreHouse> GetConfiguration()
        {
            return _configuration;
        }
    }
}