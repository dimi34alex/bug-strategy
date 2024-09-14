using System;
using BugStrategy.EntityState;
using BugStrategy.ResourceSources;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class ResourceExtractionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.ExtractionResource;

        private readonly UnitBase _unit;
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public override event Action StateExecuted;
        
        public ResourceExtractionState(UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor)
        {
            _unit = unit;
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }
        
        public override void OnStateEnter()
        {
            if (_resourceExtractionProcessor.GotResource ||
                !_unit.CurrentPathData.Target.TryCast(out ResourceSourceBase resourceSource))
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem:" +
                                 $"| {!_unit.CurrentPathData.Target.TryCast(out resourceSource)}");
#endif
                //_unit.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _resourceExtractionProcessor.OnResourceExtracted += ResourceExtracted;
            _resourceExtractionProcessor.StartExtraction(resourceSource);
        }

        public override void OnStateExit()
        {
            _resourceExtractionProcessor.OnResourceExtracted -= ResourceExtracted;
            _resourceExtractionProcessor.AbortExtraction();
        }

        public override void OnUpdate()
        {
            
        }

        private void ResourceExtracted()
        {
            // _unit.AutoGiveOrder(null);
            StateExecuted?.Invoke();
        }
    }
}