using System;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using UnityEngine;
namespace BugStrategy.Unit.Ants
{
    public class AntMoveState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Move;
        private const float DistanceBuffer = 0.1f;
        
        private readonly AntBase _ant;

        public override event Action StateExecuted;
        private event Action UpdateEvent;
        
        public AntMoveState(AntBase ant)
        {
            _ant = ant;
        }
        
        public override void OnStateEnter()
        {
            _ant.SetDestination(_ant.TargetMovePosition);
            _ant.OnTargetMovePositionChange += UpdateDestinationPosition;

            if (_ant.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _ant.CurrentProfession.OrderValidatorBase.OnEnterInZone += CheckTargetDistance;
        }

        public override void OnStateExit()
        {
            _ant.SetDestination(_ant.Transform.position);
            _ant.OnTargetMovePositionChange -= UpdateDestinationPosition;
            
            UpdateEvent -= ManualCheckDistance;
            _ant.CurrentProfession.OrderValidatorBase.OnEnterInZone -= CheckTargetDistance;
        }

        public override void OnUpdate() => UpdateEvent?.Invoke();

        private void UpdateDestinationPosition()
        {
            _ant.SetDestination(_ant.TargetMovePosition);
            
            UpdateEvent -= ManualCheckDistance;
            _ant.CurrentProfession.OrderValidatorBase.OnEnterInZone -= CheckTargetDistance;

            if (_ant.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _ant.CurrentProfession.OrderValidatorBase.OnEnterInZone += CheckTargetDistance;
        }

        private void ManualCheckDistance()
        {
            if(Vector3.Distance(_ant.Transform.position, _ant.TargetMovePosition) < DistanceBuffer)
                StateExecuted?.Invoke();
        }
        
        private void CheckTargetDistance()
        {
            StateExecuted?.Invoke();
        }
    }
}