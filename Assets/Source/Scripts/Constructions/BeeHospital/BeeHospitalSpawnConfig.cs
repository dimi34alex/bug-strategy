using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHospital
{
    [CreateAssetMenu(fileName = nameof(BeeHospitalSpawnConfig), menuName = "Configs/Constructions/SpawnConfigs/" + nameof(BeeHospitalSpawnConfig))]
    public class BeeHospitalSpawnConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public ConstructionSpawnConfiguration<BeeHospital> Config { get; private set; }
    }
}