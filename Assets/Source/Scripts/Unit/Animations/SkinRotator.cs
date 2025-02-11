using BugStrategy.EntityState;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace BugStrategy.Unit.Animations
{
    public class SkinRotator : MonoBehaviour
    {
        [SerializeField] private UnitBase unitBase;

        private bool isMovingToNewPos = false;

        private void Awake ()
        {
            if(unitBase == null)
                unitBase = transform.parent.GetComponent<UnitBase>();
        }

        private void Start ()
        {
            unitBase.OnTargetMovePositionChange += UpdateIsMovingToNewPos;
        }

        private void FixedUpdate ()
        {
            if(unitBase.StateMachine.ActiveState.Equals(EntityStateID.Move))
                UpdateOrientationByPos();
            else
            {
                isMovingToNewPos = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            }
        }

        private void OnDestroy ()
        {
            unitBase.OnTargetMovePositionChange -= UpdateIsMovingToNewPos;
        }

        private void UpdateOrientationByPos ()
        {
            if(!isMovingToNewPos)
            {
                Vector3 dir = (unitBase.TargetMovePosition - unitBase.Transform.position);
                float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

                InversScaleX(angle);

                if(angle >= 90 || angle <= -90)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, angle);
                else
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);

                isMovingToNewPos = true;
            }
        }

        private void InversScaleX (float angle)
        {
            var scale = transform.localScale;

            if((scale.x < 0 && (angle >= 90 || angle <= -90)) || (scale.x > 0 && (angle > -90 && angle < 90)))
            {
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        private void UpdateIsMovingToNewPos ()
        {
            isMovingToNewPos = false;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
}