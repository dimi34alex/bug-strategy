using System;
using Unit.ProfessionsCore;
using UnityEngine;

namespace Unit.States
{
    public class MoveState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Move;

        private const float DistanceBuffer = 0.1f;
        
        private readonly UnitBase _unit;
        private readonly ProfessionBase _profession;
        
        private event Action UpdateEvent;
        
        public MoveState(UnitBase unit, ProfessionBase profession)
        {
            _unit = unit;
            _profession = profession;
        }
        
        public override void OnStateEnter()
        {
            _unit.SetDestination(_unit.TargetMovePosition);
            _unit.OnTargetMovePositionChange += UpdateDestinationPosition;

            if (_unit.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _profession.OnEnterInZone += CheckTargetDistance;
        }

        public override void OnStateExit()
        {
            _unit.SetDestination(_unit.Transform.position);
            _unit.OnTargetMovePositionChange -= UpdateDestinationPosition;
            
            UpdateEvent -= ManualCheckDistance;
            _profession.OnEnterInZone -= CheckTargetDistance;
        }

        public override void OnUpdate() => UpdateEvent?.Invoke();

        private void UpdateDestinationPosition()
        {
            _unit.SetDestination(_unit.TargetMovePosition);
            
            UpdateEvent -= ManualCheckDistance;
            _profession.OnEnterInZone -= CheckTargetDistance;

            if (_unit.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _profession.OnEnterInZone += CheckTargetDistance;
        }

        private void ManualCheckDistance()
        {
            if (Vector3.Distance(_unit.Transform.position, _unit.TargetMovePosition) < DistanceBuffer)
                _unit.HandleGiveOrder(_unit.CurrentPathData.Target, _unit.CurrentPathData.PathType);
        }
        
        private void CheckTargetDistance()
        {
            if (_profession.CheckDistance(_unit.CurrentPathData))
                _unit.HandleGiveOrder(_unit.CurrentPathData.Target, _unit.CurrentPathData.PathType);
        }
    }
}
