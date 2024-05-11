using System;
using Construction.TownHalls;
using Unit.Ants.Professions;
using Unit.OrderValidatorCore;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntStorageResourceState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.StorageResource;

        private readonly AntBase _ant;

        private AntWorkerProfession _workerProfession;
        
        public override event Action StateExecuted;
        
        public AntStorageResourceState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            if (_ant.CurProfessionType != ProfessionType.Worker ||
                !_ant.CurrentProfession.TryCast(out _workerProfession) ||
                !_workerProfession.ResourceExtractionProcessor.GotResource ||
                !_ant.CurrentPathData.Target.CastPossible<TownHallBase>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurProfessionType} | " +
                                 $"{!_ant.CurrentProfession.TryCast(out _workerProfession)} | " +
                                 $"{!_ant.CurrentPathData.Target.CastPossible<TownHallBase>()}");            
#endif
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _workerProfession.ResourceExtractionProcessor.StorageResources();
            
            // _ant.AutoGiveOrder(null);
            StateExecuted?.Invoke();
        }

        public override void OnStateExit()
        {

        }

        public override void OnUpdate()
        {
            
        }
    }
}