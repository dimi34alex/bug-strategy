using Unit.Professions;
using UnityEngine;

namespace Unit.Ants.ProfessionsConfigs
{
    [CreateAssetMenu(fileName = "AntWorkerConfig", menuName = "Configs/Ant Professions/Worker")]
    public class AntWorkerConfig : AntProfessionConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.Worker;
        
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}