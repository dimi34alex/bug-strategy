using System;
using BugStrategy.Constructions;
using BugStrategy.EntityState;
using BugStrategy.Unit.ProcessorsCore;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class StorageResourceState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.StorageResource;

        private readonly UnitBase _unit;
        private readonly ResourceExtractionProcessor _resourceExtractionProcessor;
        
        public override event Action StateExecuted;
        
        public StorageResourceState(UnitBase unit, ResourceExtractionProcessor resourceExtractionProcessor)
        {
            _unit = unit;
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }
        
        public override void OnStateEnter()
        {
            if (!_resourceExtractionProcessor.GotResource ||
                !_unit.CurrentPathData.Target.CastPossible<TownHallBase>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{!_unit.CurrentPathData.Target.CastPossible<TownHallBase>()}");            
#endif
                //_unit.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _resourceExtractionProcessor.StorageResources();
            
            //_unit.AutoGiveOrder(null);
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