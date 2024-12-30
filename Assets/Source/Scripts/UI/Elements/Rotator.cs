using DG.Tweening;
using UnityEngine;

namespace BugStrategy.UI.Elements
{
    [RequireComponent(typeof(RectTransform))]
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationPerSecond;
        [SerializeField] private bool isClockwise;

        private RectTransform _rectTransform;
        private Tween _tween;

        private void Awake() 
            => _rectTransform = GetComponent<RectTransform>();

        private void OnEnable() 
            => StartAnimation();

        private void OnDisable() 
            => _tween?.Kill();

        private void StartAnimation()
        {
            _tween?.Kill();

            var angle = isClockwise ? 360 : -360;
            
            _tween = _rectTransform
                .DORotate(new Vector3(0, 0, angle), 1f / rotationPerSecond, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
        }
    }
}