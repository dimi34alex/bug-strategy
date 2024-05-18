using UnityEngine;

public class SphereTrigger : TriggerZone
{
    [SerializeField] private SphereCollider sphereCollider;

    public void SetRadius(float newRadius)
        => sphereCollider.radius = newRadius;
}