using System;
using UnityEngine;

namespace Unit.Ants.States
{
    public class AntMoveState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.Move;

        private const float DistanceBuffer = 0.1f;
        
        private readonly AntBase _ant;

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
                _ant.Profession.OnEnterInZone += CheckTargetDistance;
        }

        public override void OnStateExit()
        {
            _ant.SetDestination(_ant.Transform.position);
            _ant.OnTargetMovePositionChange -= UpdateDestinationPosition;
            
            UpdateEvent -= ManualCheckDistance;
            _ant.Profession.OnEnterInZone -= CheckTargetDistance;
        }

        public override void OnUpdate() => UpdateEvent?.Invoke();

        private void UpdateDestinationPosition()
        {
            _ant.SetDestination(_ant.TargetMovePosition);
            
            UpdateEvent -= ManualCheckDistance;
            _ant.Profession.OnEnterInZone -= CheckTargetDistance;

            if (_ant.CurrentPathData.Target.IsAnyNull())
                UpdateEvent += ManualCheckDistance;
            else
                _ant.Profession.OnEnterInZone += CheckTargetDistance;
        }

        private void ManualCheckDistance()
        {
            if(Vector3.Distance(_ant.Transform.position, _ant.TargetMovePosition) < DistanceBuffer)
                _ant.HandleGiveOrder(_ant.CurrentPathData.Target, _ant.CurrentPathData.PathType);
        }
        
        private void CheckTargetDistance()
        {
            if (_ant.Profession.CheckDistance(_ant.CurrentPathData))
                _ant.HandleGiveOrder(_ant.CurrentPathData.Target, _ant.CurrentPathData.PathType);
        }
    }
}
