using BugStrategy.EntityState;
using UnityEngine;

namespace BugStrategy.Unit.Animations
{
    public class SkinRotator : MonoBehaviour
    {
        [SerializeField] private UnitBase unitBase;
        [SerializeField] private SkinRotatorConfig config;

        private bool _isMovingToNewPos = false;

        private void Awake()
        {
            if (unitBase == null)
                unitBase = transform.parent.GetComponent<UnitBase>();
        }

        private void Start()
        {
            unitBase.OnTargetMovePositionChange += UpdateIsMovingToNewPos;
        }

        private void FixedUpdate()
        {
            if (unitBase.StateMachine.ActiveState.Equals(EntityStateID.Move))
                UpdateOrientationByPos();
            else
            {
                if (_isMovingToNewPos)
                {
                    _isMovingToNewPos = false;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                }
            }
        }

        private void OnDestroy()
        {
            unitBase.OnTargetMovePositionChange -= UpdateIsMovingToNewPos;
        }

        private void UpdateOrientationByPos()
        {
            if (!_isMovingToNewPos)
            {
                var dir = (unitBase.TargetMovePosition - unitBase.Transform.position);
                var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

                if (angle > 90 || angle < -90)
                {
                    var sign = Mathf.Sign(angle);
                    angle = Mathf.Clamp(angle, sign * 180 - config.AngleClamp, sign * 180 + config.AngleClamp);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle + 180);
                }
                else
                {
                    angle = Mathf.Clamp(angle, -config.AngleClamp, config.AngleClamp);
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
                }

                InverseScaleX();
                _isMovingToNewPos = true;
            }
        }

        private void InverseScaleX()
        {
            var targetPosition = unitBase.TargetMovePosition.x;
            var currentPosition = unitBase.Transform.position.x;

            var direction = targetPosition - currentPosition;
            if (direction != 0)
            {
                var directionSign = Mathf.Sign(direction);

                var scale = transform.localScale;
                var scaleSign = Mathf.Sign(scale.x);

                scale.x *= -1 * directionSign * scaleSign;
                transform.localScale = scale;
            }
        }

        private void UpdateIsMovingToNewPos()
        {
            _isMovingToNewPos = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);

            UpdateOrientationByPos();
        }
    }
}