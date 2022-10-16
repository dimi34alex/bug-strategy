using UnityEngine;

public abstract class ConstructionSkinBase : MonoBehaviour
{
    [SerializeField] private Pose _localPose;

    private void Awake()
    {
        transform.localPosition = _localPose.position;
        transform.localRotation = _localPose.rotation;
    }
}
