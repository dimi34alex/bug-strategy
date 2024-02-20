using Unit.Professions;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntResourceExtractionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.ExtractionResource;

        private readonly AntBase _ant;

        private WorkerProfession _workerProfession;
        
        public AntResourceExtractionState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if (_ant.CurProfessionType != ProfessionType.Worker ||
                !_ant.Profession.TryCast(out _workerProfession) ||
                _workerProfession.ResourceExtractionProcessor.GotResource ||
                !_ant.CurrentPathData.Target.TryCast(out ResourceSourceBase resourceSource))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: {_ant.CurProfessionType} " +
                                 $"| {!_ant.Profession.TryCast(out _workerProfession)} " +
                                 $"| {!_ant.CurrentPathData.Target.TryCast(out resourceSource)}");
#endif
                _ant.AutoGiveOrder(null);
                return;
            }
            
            _workerProfession.ResourceExtractionProcessor.OnResourceExtracted += ResourceExtracted;
            _workerProfession.ResourceExtractionProcessor.StartExtraction(resourceSource.ResourceID);
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
            _ant.AutoGiveOrder(null);
        }
    }
}