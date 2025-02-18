using System;
using System.Collections.Generic;
using BugStrategy.CustomInput;
using BugStrategy.Selection;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private float CameraMoveSpeed;
        [SerializeField] private float PercentageOfScreen;

        [Inject] private readonly IInputProvider _inputProvider;
        [Inject] private readonly IReadOnlyCameraBounds _cameraBounds;

        private float _sizeBorderToMove;
        private float _xDist, _yDist;
        private Vector2 _delta;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        [SerializeField] private Selector _selector;
        private Vector3 OldCamPos;

        private IReadOnlyList<Vector3> Bounds => _cameraBounds.Bounds;

        private void Awake ()
        {
            _sizeBorderToMove = Mathf.Min(Screen.width, Screen.height) * (PercentageOfScreen / 100);
        }

        private void LateUpdate ()
        {
            Move();
        }

        private void Move ()
        {
            OldCamPos = transform.position;

            if(_inputProvider.ScrollDown || _inputProvider.ScrollHold)
                MoveByMouseWheel();
            else if(CursorOnScreen() && !_inputProvider.MouseCursorOverUi())
                MoveByCursor();

            if(OldCamPos != transform.position)//7 - 77 . 12 - 45 . 17 - 32
            {
                _selector.UpdateWorldStartPos((OldCamPos - transform.position) * CheckCurCamSize());
                Debug.Log(CheckCurCamSize());
            }
        }

        private float CheckCurCamSize ()
        {
            switch(_camera.orthographicSize)
            {
                case 17:
                    return 32f;
                case 16:
                    return 34f;
                case 15:
                    return 36f;
                case 14:
                    return 39f;  
                case 13:
                    return 41.5f;
                case 12:
                    return 45f; 
                case 11:
                    return 49.5f;
                case 10:
                    return 54f;
                case 9:
                    return 60f;
                case 8:
                    return 68f;
                case 7:
                    return 77f;
                default:
                    return 45f;
            };
        }

        private void MoveByMouseWheel ()
        {
            _targetPosition.y = transform.position.y;

            if(_inputProvider.ScrollDown)
                _startPosition = transform.position + _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();

            if(_inputProvider.ScrollHold)
            {
                _targetPosition = _startPosition - _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();

                Vector3 position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);

                position = position.Clamp(Bounds[0], Bounds[1]);

                transform.position = position;
            }
        }

        private void MoveByCursor ()
        {
            if(CheckIfCursorInBorderZone())
            {
                _delta = _delta.normalized;
                _delta *= Mathf.Clamp01(1 - Mathf.Min(_xDist, _yDist) / _sizeBorderToMove);

                transform.Translate(_delta * (CameraMoveSpeed * Time.deltaTime), Space.Self);
                transform.position = transform.position.Clamp(Bounds[0], Bounds[1]);
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

        private bool CursorOnScreen ()
        {
            Vector2 mousePos = _inputProvider.MousePosition;

            return !(mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height);
        }
    }
}