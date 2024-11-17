using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntWorkerConfig), menuName = "Configs/Units/Ants/" + nameof(AntWorkerConfig))]
    public class AntWorkerConfig : AntProfessionConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.Worker;
        
        [field: SerializeField] public int GatheringCapacity { get; private set; }
        [field: SerializeField] public float GatheringTime { get; private set; }
    }
}