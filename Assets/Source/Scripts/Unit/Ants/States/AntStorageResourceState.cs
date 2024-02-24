using Unit.ProfessionsCore;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntStorageResourceState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.StorageResource;

        private readonly AntBase _ant;

        private WorkerProfession _workerProfession;
        
        public AntStorageResourceState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            if (_ant.CurProfessionType != ProfessionType.Worker ||
                !_ant.Profession.TryCast(out _workerProfession) ||
                !_workerProfession.ResourceExtractionProcessor.GotResource ||
                !_ant.CurrentPathData.Target.CastPossible<TownHall>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurProfessionType} | " +
                                 $"{!_ant.Profession.TryCast(out _workerProfession)} | " +
                                 $"{!_ant.CurrentPathData.Target.CastPossible<TownHall>()}");            
#endif
                _ant.AutoGiveOrder(null);
                return;
            }
            
            ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, _workerProfession.ResourceExtractionProcessor.ExtractionCapacity);
            _workerProfession.ResourceExtractionProcessor.StorageResources();
            
            _ant.AutoGiveOrder(null);
        }

        public override void OnStateExit()
        {

        }

        public override void OnUpdate()
        {
            
        }
    }
}