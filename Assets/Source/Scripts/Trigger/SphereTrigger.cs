using UnityEngine;

namespace BugStrategy.Trigger
{
    public class SphereTrigger : TriggerZone
    {
        private SphereCollider _sphereCollider;

        private void Awake() 
            => _sphereCollider = GetComponent<SphereCollider>();

        public void SetRadius(float newRadius)
            => _sphereCollider.radius = newRadius;
    }
}