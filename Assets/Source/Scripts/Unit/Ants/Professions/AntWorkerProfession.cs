using Unit.Ants.Configs.Professions;
using Unit.ProfessionsCore;
using Unit.ProfessionsCore.Processors;
using UnityEngine;

namespace Unit.Ants.Professions
{
    public sealed class AntWorkerProfession : AntProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.Worker;

        public override OrderValidatorBase OrderValidatorBase { get; }
        public readonly ResourceExtractionProcessor ResourceExtractionProcessor;
        
        public AntWorkerProfession(AntBase ant, AntWorkerConfig antHandItem, ResourceRepository resourceRepository,
            GameObject resourceSkin)
            : base(antHandItem.AntProfessionRang)
        {
            ResourceExtractionProcessor = new ResourceExtractionProcessor(antHandItem.GatheringCapacity,
                antHandItem.GatheringTime, resourceRepository, resourceSkin);

            OrderValidatorBase = new WorkerOrderValidator(ant, antHandItem.InteractionRange, ResourceExtractionProcessor);
            
            OrderValidatorBase.OnEnterInZone += EnterInZone;
        }

        public override void HandleUpdate(float time)
        {
            base.HandleUpdate(time);
            
            ResourceExtractionProcessor.HandleUpdate(time);
        }
    }
}