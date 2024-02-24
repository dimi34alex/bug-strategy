using UnityEngine;

namespace Unit.States
{
    public class BuildState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Build;

        private readonly MovingUnit _unit;
        private BuildingProgressConstruction _buildingProgressConstruction;
        
        public BuildState(MovingUnit unit)
        {
            _unit = unit;
        }
        
        public override void OnStateEnter()
        {
            if (_unit.CurrentPathData.TargetType != UnitTargetType.Construction ||
                _unit.CurrentPathData.Target.IsAnyNull() ||
                !_unit.CurrentPathData.Target.TryCast(out _buildingProgressConstruction))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_unit.CurrentPathData.TargetType} | " +
                                 $"{_unit.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_unit.CurrentPathData.Target.TryCast(out _buildingProgressConstruction)}");
                
                _unit.AutoGiveOrder(null);
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

            _unit.AutoGiveOrder(null);
        }
    }
}