using System;
using BugStrategy.Constructions;
using BugStrategy.EntityState;
using BugStrategy.Unit.AbilitiesCore;
using BugStrategy.Unit.ProcessorsCore;
using BugStrategy.Unit.Repairing;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class RepairState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Repair;

        private readonly UnitBase _unit;
        private readonly RepairProcessor _repairProcessor;

        public override event Action StateExecuted;
        
        public RepairState(UnitBase unit, RepairProcessor repairProcessor)
        {
            _unit = unit;
            _repairProcessor = repairProcessor;
        }
        
        public override void OnStateEnter()
        {
            if (!_unit.CurrentPathData.Target.CastPossible<ConstructionBase>())
            {
#if UNITY_EDITOR
                Debug.LogWarning($"Some problem: " +
                                 $"{!_unit.CurrentPathData.Target.CastPossible<ConstructionBase>()}");            
#endif
                StateExecuted?.Invoke();
                return;
            }

            _repairProcessor.SetConstruction(_unit.CurrentPathData.Target.Cast<ConstructionBase>());
        }

        public override void OnStateExit()
        {
            _repairProcessor.SetConstruction(null);
        }

        public override void OnUpdate()
        {
            _repairProcessor.HandleUpdate(Time.deltaTime);

            if ( _repairProcessor.Construction == null ||  
                 _repairProcessor.Construction.HealthStorage.FillPercentage >= 1)
                StateExecuted?.Invoke();
        }
    }
}