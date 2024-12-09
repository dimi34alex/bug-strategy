using UnityEngine;

namespace BugStrategy.ObjectAiming
{
    public class AimingObject : MonoBehaviour, IAimingObject
    {
        private Material _material;
        
        private static readonly int Enable = Shader.PropertyToID("_Enable");

        private void Awake()
        {
            if (gameObject.GetComponent<Renderer>()!= null)
                _material = gameObject.GetComponent<Renderer>().material;
            else
                _material = gameObject.GetComponentInChildren<Renderer>().material;
      
            _material.SetInt(Enable, 0);
        }

        public void OnPointerEnter() 
            => _material.SetInt(Enable, 1);

        public void OnPointerExit() 
            => _material.SetInt(Enable, 0);
    }
}
