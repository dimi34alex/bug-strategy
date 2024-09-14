using System;
using BugStrategy.Constructions;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit
{
    public class BuildState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Build;

        private readonly UnitBase _unit;
        private BuildingProgressConstruction _buildingProgressConstruction;
        
        public override event Action StateExecuted;
        
        public BuildState(UnitBase unit)
        {
            _unit = unit;
        }
        
        public override void OnStateEnter()
        {
            if (_unit.CurrentPathData.TargetType != TargetType.Construction ||
                _unit.CurrentPathData.Target.IsAnyNull() ||
                !_unit.CurrentPathData.Target.TryCast(out _buildingProgressConstruction))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_unit.CurrentPathData.TargetType} | " +
                                 $"{_unit.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_unit.CurrentPathData.Target.TryCast(out _buildingProgressConstruction)}");
                
                // _unit.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            _buildingProgressConstruction.WorkerArrived = true;
            _buildingProgressConstruction.OnTimerEnd += EndOfBuildConstruction;
        }

        public override void OnStateExit()
        {
            _buildingProgressConstruction.WorkerArrived = false;
            _buildingProgressConstruction.OnTimerEnd -= EndOfBuildConstruction;
        }

        public override void OnUpdate()
        {
            //TODO: update construction building progress in the unit logic, at the moment this logic on the construction side
        }

        private void EndOfBuildConstruction(BuildingProgressConstruction buildingProgressConstruction)
        {
            buildingProgressConstruction.WorkerArrived = false;

            // _unit.AutoGiveOrder(null);
            StateExecuted?.Invoke();
        }
    }
}