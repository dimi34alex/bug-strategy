using BugStrategy.CustomInput;
using CycleFramework.Extensions;
using System.Collections;
using System.Threading;
using UnityEngine;
using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject field;

        [SerializeField] private float CameraMoveSpeed;
        [SerializeField] private float PercentageOfScreen;

        private float SizeBorderToMove;
        private float xDist, yDist;

        [Inject] private readonly IInputProvider _inputProvider;

        private Vector2 delta;
        private Vector3[] _bounds;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private void Awake ()
        {
            _bounds = new Vector3[2];
            _bounds[0] = new Vector3(field.transform.localScale.x * -5f, -100f, field.transform.localScale.z * -5f);
            _bounds[1] = new Vector3(field.transform.localScale.x * 5f, 100f, field.transform.localScale.z * 5f);
            SizeBorderToMove = Mathf.Min(Screen.width, Screen.height) * (PercentageOfScreen / 100);
        }

        private void LateUpdate ()
        {
            MoveByCursor();
            Move();
        }

        private void Move ()
        {
            if(CheckIfCursorInBorderZone())
                return;

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
            if(_inputProvider.ScrollDown || _inputProvider.ScrollHold)
                return;

            if(CheckIfCursorInBorderZone())
            {
                delta = delta.normalized;
                delta *= Mathf.Clamp01(1 - Mathf.Min(xDist, yDist) / SizeBorderToMove);

                transform.Translate(delta * CameraMoveSpeed * Time.deltaTime, Space.Self);
                transform.position = transform.position.Clamp(_bounds[0], _bounds[1]);
            }
        }

        private bool CheckIfCursorInBorderZone ()
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;

            delta = mousePos - new Vector2(0.5f, 0.5f);

            xDist = Screen.width * (0.5f - Mathf.Abs(delta.x));
            yDist = Screen.height * (0.5f - Mathf.Abs(delta.y));

            if(xDist < SizeBorderToMove || yDist < SizeBorderToMove)
                return true;
            return false;
        }
    }
}