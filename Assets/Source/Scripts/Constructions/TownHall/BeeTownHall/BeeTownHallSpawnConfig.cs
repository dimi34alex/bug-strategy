using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = "TownHallConfig", menuName = "Config/TownHallConfig")]
    public class BeeTownHallSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeTownHall> _configuration;

        public ConstructionSpawnConfiguration<BeeTownHall> GetConfiguration()
        {
            return _configuration;
        }
    }
}