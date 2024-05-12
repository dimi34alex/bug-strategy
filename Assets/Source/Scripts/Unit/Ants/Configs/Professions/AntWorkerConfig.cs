using Unit.Ants.Professions;
using UnityEngine;

namespace Unit.Ants.Configs.Professions
{
    [CreateAssetMenu(fileName = nameof(AntWorkerConfig), menuName = "Configs/Units/Ants/" + nameof(AntWorkerConfig))]
    public class AntWorkerConfig : AntProfessionConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.Worker;
        
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}