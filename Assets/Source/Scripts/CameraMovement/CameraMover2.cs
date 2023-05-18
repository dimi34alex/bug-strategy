using UnityEngine;

public class CameraMover2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject field;

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
    {
        Move();
    }

    private void Move()
    {
        _targetPosition.y = transform.position.y;

        if (Input.GetMouseButtonDown(2))
            _startPosition = transform.position + _camera.ScreenToWorldPoint(Input.mousePosition).XZ();

        if (Input.GetMouseButton(2))
            _targetPosition = _startPosition - _camera.ScreenToWorldPoint(Input.mousePosition).XZ();

        Vector3 position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _moveSpeed);

        position = position.Clamp(_bounds[0], _bounds[1]);

        transform.position = position;
    }
}