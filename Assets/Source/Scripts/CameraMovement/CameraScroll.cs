using System.Collections;
using UnityEngine;

namespace BugStrategy.CameraMovement
{
    public class CameraScroll : MonoBehaviour
    {
        [SerializeField] private bool useScroll;
        [SerializeField] private float scrollPower;
        [SerializeField] private float cameraSizeOffset;
    
        private Camera _camera;
        private float _startCameraSize;
        private bool _coroutineIsActive;
        private float _targetSize;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _startCameraSize = _camera.orthographicSize;
            _targetSize = _startCameraSize;
        }

        private void Update() 
            => CameraScrolling();

        private void OnDisable()
        {
            StopCoroutine(ScrollLerp());
            _camera.orthographicSize = _targetSize;
        }
    
        private void CameraScrolling()
        {
            if (!useScroll)
                return;
        
            float scrollDirection = Input.mouseScrollDelta.y;
            if(scrollDirection == 0)
                return;
        
            _targetSize -= scrollDirection * scrollPower;
            _targetSize = Mathf.Clamp(_targetSize, _startCameraSize - cameraSizeOffset, _startCameraSize + cameraSizeOffset);
    
            if(!_coroutineIsActive)
                StartCoroutine(ScrollLerp());
        }

        private IEnumerator ScrollLerp()
        {
            _coroutineIsActive = true;
        
            while (Mathf.Abs(_camera.orthographicSize - _targetSize) > 0.01f)
            {
                float newSize = Mathf.Lerp(_camera.orthographicSize,_targetSize, 3f * Time.deltaTime);
                _camera.orthographicSize = newSize;
                yield return new WaitForEndOfFrame();
            }
        
            _camera.orthographicSize = _targetSize;
            _coroutineIsActive = false;
        }
    }
}