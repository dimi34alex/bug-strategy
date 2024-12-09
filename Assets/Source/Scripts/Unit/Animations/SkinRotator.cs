using UnityEngine;

namespace BugStrategy.Unit.Animations
{
    public class SkinRotator : MonoBehaviour
    {
        [SerializeField] private UnitBase unitBase;

        private float _prevDirectionSign;
        private float _initialSign;

        private void Awake()
        {
            if (unitBase == null)
                unitBase = transform.parent.GetComponent<UnitBase>();
        }

        private void Start()
        {
            unitBase.OnTargetMovePositionChange += UpdateOrientationByPos;

            InitializeOrientation();
        }

        private void OnDestroy()
        {
            unitBase.OnTargetMovePositionChange -= UpdateOrientationByPos;
        }

        private void InitializeOrientation()
        {
            var sign = Mathf.Sign(transform.localScale.x);

            _prevDirectionSign = _initialSign = sign;
        }

        private void UpdateOrientationByPos()
        {
            var targetPosition = unitBase.TargetMovePosition.x;
            var currentPosition = unitBase.Transform.position.x;

            var direction = targetPosition - currentPosition;
            var directionSign = -_initialSign * Mathf.Sign(direction);
            
            if (direction != 0 && (directionSign + _prevDirectionSign) == 0)
            {
                _prevDirectionSign = directionSign;
                
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }
    }
}