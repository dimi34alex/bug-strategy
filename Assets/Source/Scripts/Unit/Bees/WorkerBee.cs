using System.Collections.Generic;
using Unit.Bees.Configs;
using Unit.ProfessionsCore;
using Unit.States;
using UnityEngine;

namespace Unit.Bees
{
    public class WorkerBee : BeeUnit
    {
        [SerializeField] private BeeWorkerConfig beeWorkerConfig;
        [SerializeField] private GameObject resourceSkin;

        public override UnitType UnitType => UnitType.WorkerBee;
        public override IReadOnlyProfession CurrentProfession => _workerProfession;

        private WorkerProfession _workerProfession;

        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage = new ResourceStorage(beeWorkerConfig.HealthPoints, beeWorkerConfig.HealthPoints);
            _workerProfession = new WorkerProfession(this, beeWorkerConfig.InteractionRange, beeWorkerConfig.GatheringCapacity, beeWorkerConfig.GatheringTime, resourceSkin);
            resourceSkin.SetActive(false);

            var stateBases = new List<EntityStateBase>()
            {
                new IdleState(),
                new MoveState(this, _workerProfession),
                new BuildState(this),
                new ResourceExtractionState(this, _workerProfession),
                new StorageResourceState(this, _workerProfession),
            };
            _stateMachine = new EntityStateMachine(stateBases, EntityStateID.Idle);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        
            _workerProfession.HandleUpdate(Time.deltaTime);
        }
    }
}
    