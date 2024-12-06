using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyTownHall
{
    [CreateAssetMenu(fileName = nameof(ButterflyTownHallSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(ButterflyTownHallSpawnConfig))]
    public class ButterflyTownHallSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<ButterflyTownHall> _configuration;

        public ConstructionSpawnConfiguration<ButterflyTownHall> GetConfiguration()
        {
            return _configuration;
        }
    }
}