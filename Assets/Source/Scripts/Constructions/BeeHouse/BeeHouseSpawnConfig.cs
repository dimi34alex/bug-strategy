using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeHouseSpawnConfig), menuName = "Config/" + nameof(BeeHouseSpawnConfig))]
    public class BeeHouseSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeHouse> _configuration;

        public ConstructionSpawnConfiguration<BeeHouse> GetConfiguration()
        {
            return _configuration;
        }
    }
}