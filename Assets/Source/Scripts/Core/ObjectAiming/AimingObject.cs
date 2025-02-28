using UnityEngine;

namespace BugStrategy.ObjectAiming
{
    public class AimingObject : MonoBehaviour, IAimingObject
    {
        private SkinOutlineHolder _skinOutlineHolder;
        
        private void Start() 
            => _skinOutlineHolder = gameObject.GetComponentInChildren<SkinOutlineHolder>();

        public void OnPointerEnter()
            => _skinOutlineHolder.ToggleOutlineVisibility(true);

        public void OnPointerExit() 
            => _skinOutlineHolder.ToggleOutlineVisibility(false);
	}
}
