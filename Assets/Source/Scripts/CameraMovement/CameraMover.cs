using BugStrategy.CustomInput;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.CameraMovement
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject field;

        [Inject] private readonly IInputProvider _inputProvider;

        private Vector3[] _bounds;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _bounds = new Vector3[2];
            _bounds[0] = new Vector3(field.transform.localScale.x * -5f, -100f, field.transform.localScale.z * -5f);
            _bounds[1] = new Vector3(field.transform.localScale.x * 5f, 100f, field.transform.localScale.z * 5f);
        }

        private void LateUpdate() 
            => Move();

        private void Move()
        {
            _targetPosition.y = transform.position.y;

            if (_inputProvider.ScrollDown)
                _startPosition = transform.position + _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();

            if (_inputProvider.ScrollHold)
                _targetPosition = _startPosition - _camera.ScreenToWorldPoint(_inputProvider.MousePosition).XZ();

            Vector3 position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);

            position = position.Clamp(_bounds[0], _bounds[1]);

            transform.position = position;
        }
    }
}