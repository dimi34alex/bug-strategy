using System.Collections.Generic;
using BugStrategy.CustomInput;
using CycleFramework.Execute;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraMover : CycleInitializerBase
    {
        [SerializeField] private CameraMoveConfig cameraMoveConfig;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly IReadOnlyCameraBounds _cameraBounds;
        
        private float MoveSpeedByWheel => cameraMoveConfig.MoveSpeedByWheel;
        private float MoveSpeedByCursor => cameraMoveConfig.MoveSpeedByCursor;
        private float PercentageOfScreen => cameraMoveConfig.PercentageOfScreen;
        private Transform CameraTransform => _camera.transform;

        private bool _isMoveProcessByWheel;
        private Camera _camera;
        private float _sizeBorderToMove;
        private float _xDist, _yDist;
        private Vector2 _delta;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        
        private IReadOnlyList<Vector3> Bounds => _cameraBounds.Bounds;

        private void Awake ()
        {
            _camera = Camera.main;
            _sizeBorderToMove = Mathf.Min(Screen.width, Screen.height) * (PercentageOfScreen / 100);            
        }

        protected override void OnLateUpdate()
        {
            var isStartMoveByWheel = _inputProvider.ScrollDown && !_inputProvider.MouseCursorOverUi();
            var isMoveProcessByWheel = _inputProvider.ScrollHold && _isMoveProcessByWheel;
            
            if(isStartMoveByWheel || isMoveProcessByWheel)
                MoveByMouseWheel();
            else
            {
                _isMoveProcessByWheel = false;
                if (CursorOnScreen())
                    MoveByCursor();
            }
        }

        private void MoveByMouseWheel()
        {
            _targetPosition.y = CameraTransform.position.y;

            if (_inputProvider.ScrollDown)
            {
                _isMoveProcessByWheel = true;
                _startPosition = CameraTransform.position + _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();
            }

            if(_inputProvider.ScrollHold)
            {
                _targetPosition = _startPosition - _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();

                Vector3 position = Vector3.Lerp(CameraTransform.position, _targetPosition, Time.deltaTime * MoveSpeedByWheel);

                position = position.Clamp(Bounds[0], Bounds[1]);

                CameraTransform.position = position;
            }
        }

        private void MoveByCursor ()
        {
            if(CheckIfCursorInBorderZone())
            {
                _delta = _delta.normalized;
                _delta *= Mathf.Clamp01(1 - Mathf.Min(_xDist, _yDist) / _sizeBorderToMove);

                CameraTransform.Translate(_delta * (MoveSpeedByCursor * Time.deltaTime), Space.Self);
                CameraTransform.position = CameraTransform.position.Clamp(Bounds[0], Bounds[1]);
            }
        }

        private bool CheckIfCursorInBorderZone ()
        {
            Vector2 mousePos = _inputProvider.MousePosition;
            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;

            _delta = mousePos - new Vector2(0.5f, 0.5f);

            _xDist = Screen.width * (0.5f - Mathf.Abs(_delta.x));
            _yDist = Screen.height * (0.5f - Mathf.Abs(_delta.y));

            if(_xDist < _sizeBorderToMove || _yDist < _sizeBorderToMove)
                return true;
            return false;
        }

        private bool CursorOnScreen()
        {
            Vector2 mousePos = _inputProvider.MousePosition;

            return !(mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height);
        }
    }
}