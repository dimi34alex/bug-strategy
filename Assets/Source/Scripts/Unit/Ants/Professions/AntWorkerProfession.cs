using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit.OrderValidatorCore;
using BugStrategy.Unit.ProcessorsCore;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    public sealed class AntWorkerProfession : AntProfessionBase
    {
        public override ProfessionType ProfessionType => ProfessionType.Worker;

        public override OrderValidatorBase OrderValidatorBase { get; }
        public readonly ResourceExtractionProcessor ResourceExtractionProcessor;
        
        public AntWorkerProfession(AntBase ant, int professionRang, AntWorkerConfig antHandItem, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage,
            GameObject resourceSkin)
            : base(professionRang)
        {
            ResourceExtractionProcessor = new ResourceExtractionProcessor(ant, antHandItem.GatheringCapacity,
                antHandItem.GatheringTime, teamsResourcesGlobalStorage, resourceSkin);

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