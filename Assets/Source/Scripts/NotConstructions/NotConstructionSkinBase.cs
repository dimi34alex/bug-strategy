using UnityEngine;

namespace BugStrategy.NotConstructions
{
    public abstract class NotConstructionSkinBase : MonoBehaviour
    {
        [SerializeField] private Pose _localPose;

        private void Awake()
        {
            transform.localPosition = _localPose.position;
            transform.localRotation = _localPose.rotation;
        }
    }
}
