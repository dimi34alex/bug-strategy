using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BeeTownHall
{
    [CreateAssetMenu(fileName = nameof(BeeTownHallSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeTownHallSpawnConfig))]
    public class BeeTownHallSpawnConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private ConstructionSpawnConfiguration<BeeTownHall> _configuration;

        public ConstructionSpawnConfiguration<BeeTownHall> GetConfiguration()
        {
            return _configuration;
        }
    }
}