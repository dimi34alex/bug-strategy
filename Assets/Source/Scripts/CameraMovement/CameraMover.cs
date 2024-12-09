using BugStrategy.CustomInput;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private GameObject field;
        [Space]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private float CameraMoveSpeed;
        [SerializeField] private float PercentageOfScreen;

        [Inject] private readonly IInputProvider _inputProvider;

        private float _sizeBorderToMove;
        private float _xDist, _yDist;
        private Vector2 _delta;
        private Vector3[] _bounds;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private void Awake ()
        {
            _bounds = new Vector3[2];
            _bounds[0] = new Vector3(field.transform.localScale.x * -5f, -100f, field.transform.localScale.z * -5f);
            _bounds[1] = new Vector3(field.transform.localScale.x * 5f, 100f, field.transform.localScale.z * 5f);
            _sizeBorderToMove = Mathf.Min(Screen.width, Screen.height) * (PercentageOfScreen / 100);
        }

        private void LateUpdate ()
        {
            if(_inputProvider.ScrollDown || _inputProvider.ScrollHold)
                MoveByMouseWheel();
            else if(CursorOnScreen() && !_inputProvider.MouseCursorOverUi())
                MoveByCursor();
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

                position = position.Clamp(_bounds[0], _bounds[1]);

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
                transform.position = transform.position.Clamp(_bounds[0], _bounds[1]);
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