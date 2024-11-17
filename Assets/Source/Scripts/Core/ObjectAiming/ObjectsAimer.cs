using BugStrategy.CustomInput;
using UnityEngine;
using Zenject;

namespace BugStrategy.ObjectAiming
{
    public class ObjectsAimer : MonoBehaviour
    {
        [SerializeField] private LayerMask layers;

        [Inject] private IInputProvider _inputProvider;

        private IAimingObject _aimingObject;
        
        private void LateUpdate()
        {
            var ray = Camera.main.ScreenPointToRay(_inputProvider.MousePosition);
            if (Physics.Raycast(ray, out var hit, 20, layers))
            {
                var objectForAiming = hit.collider.gameObject.GetComponent<IAimingObject>();
                if (_aimingObject != objectForAiming)
                {
                    _aimingObject?.OnPointerExit();
                    _aimingObject = objectForAiming;
                    _aimingObject?.OnPointerEnter();
                }
            }
            else
            {
                _aimingObject?.OnPointerExit();
                _aimingObject = null;
            }
        }
    }
}