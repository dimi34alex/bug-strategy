using System;
using Unit.Ants.Professions;
using Unit.OrderValidatorCore;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntResourceExtractionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.ExtractionResource;

        private readonly AntBase _ant;

        private AntWorkerProfession _workerProfession;
        
        public override event Action StateExecuted;
        
        public AntResourceExtractionState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if (_ant.CurProfessionType != ProfessionType.Worker ||
                !_ant.CurrentProfession.TryCast(out _workerProfession) ||
                _workerProfession.ResourceExtractionProcessor.GotResource ||
                !_ant.CurrentPathData.Target.TryCast(out ResourceSourceBase resourceSource))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: {_ant.CurProfessionType} " +
                                 $"| {!_ant.CurrentProfession.TryCast(out _workerProfession)} " +
                                 $"| {!_ant.CurrentPathData.Target.TryCast(out resourceSource)}");
#endif
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _workerProfession.ResourceExtractionProcessor.OnResourceExtracted += ResourceExtracted;
            _workerProfession.ResourceExtractionProcessor.StartExtraction(resourceSource);
        }

        public override void OnStateExit()
        {
            _workerProfession.ResourceExtractionProcessor.OnResourceExtracted -= ResourceExtracted;
            _workerProfession.ResourceExtractionProcessor.AbortExtraction();
        }

        public override void OnUpdate()
        {
            
        }

        private void ResourceExtracted()
        {
            // _ant.AutoGiveOrder(null);
            StateExecuted?.Invoke();
        }
    }
}