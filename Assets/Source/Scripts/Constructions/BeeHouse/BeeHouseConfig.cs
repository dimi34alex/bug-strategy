using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeHouseConfig), menuName = "Config/" + nameof(BeeHouseConfig))]
    public class BeeHouseConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeHouse> _configuration;

        public ConstructionSpawnConfiguration<BeeHouse> GetConfiguration()
        {
            return _configuration;
        }
    }
}