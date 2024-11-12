using System;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using BugStrategy.Unit.OrderValidatorCore;
using UnityEngine;
namespace BugStrategy.Unit
{
    public class MoveState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Move;
        private const float DistanceBuffer = 0.1f;
        
        private readonly UnitBase _unit;
        private readonly OrderValidatorBase _orderValidator;
        
        public override event Action StateExecuted;
        private event Action UpdateEvent;
        
        public MoveState(UnitBase unit, OrderValidatorBase orderValidator)
        {
            _unit = unit;
            _orderValidator = orderValidator;
        }

        public override void OnStateEnter()
        {
            _unit.SetDestination(_unit.TargetMovePosition);
            _unit.OnTargetMovePositionChange += UpdateDestinationPosition;

            if (_unit.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _orderValidator.OnEnterInZone += CheckTargetDistance;
        }

        public override void OnStateExit()
        {
            _unit.SetDestination(_unit.Transform.position);
            _unit.OnTargetMovePositionChange -= UpdateDestinationPosition;

            UpdateEvent -= ManualCheckDistance;
            _orderValidator.OnEnterInZone -= CheckTargetDistance;
        }

        public override void OnUpdate() => UpdateEvent?.Invoke();

        private void UpdateDestinationPosition()
        {
            _unit.SetDestination(_unit.TargetMovePosition);

            UpdateEvent -= ManualCheckDistance;
            _orderValidator.OnEnterInZone -= CheckTargetDistance;

            if (_unit.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _orderValidator.OnEnterInZone += CheckTargetDistance;
        }

        private void ManualCheckDistance()
        {
            if (Vector3.Distance(_unit.Transform.position, _unit.TargetMovePosition) < DistanceBuffer)
                StateExecuted?.Invoke();
        }
        
        private void CheckTargetDistance()
        {
            StateExecuted?.Invoke();
        }
    }
}