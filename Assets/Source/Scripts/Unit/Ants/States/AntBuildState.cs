using System;
using BugStrategy.Constructions;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    public class AntBuildState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Build;

        private readonly AntBase _ant;
        private BuildingProgressConstruction _buildingProgressConstruction;
        
        public override event Action StateExecuted;
        
        public AntBuildState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            if (_ant.CurProfessionType != ProfessionType.Worker ||
                _ant.CurrentPathData.TargetType != TargetType.Construction ||
                _ant.CurrentPathData.Target.IsAnyNull() ||
                !_ant.CurrentPathData.Target.TryCast(out _buildingProgressConstruction))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurProfessionType} | " +
                                 $"{_ant.CurrentPathData.TargetType} | " +
                                 $"{_ant.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_ant.CurrentPathData.Target.TryCast(out _buildingProgressConstruction)}");
                
                // _ant.AutoGiveOrder(null);
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

            // _ant.AutoGiveOrder(null);
            StateExecuted?.Invoke();
        }
    }
}