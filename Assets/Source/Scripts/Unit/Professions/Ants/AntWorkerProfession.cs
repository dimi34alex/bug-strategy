using Unit.Ants;
using Unit.Ants.ProfessionsConfigs;
using UnityEngine;

namespace Unit.Professions.Ants
{
    public class AntWorkerProfession : WorkerProfession
    {
        public AntWorkerProfession(AntBase ant, AntWorkerConfig antHandItem, GameObject resourceSkin)
            : base(ant, antHandItem.InteractionRange, antHandItem.GatheringCapacity, antHandItem.GatheringTime, resourceSkin)
        { }
    }
}