using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonProvider : MonoBehaviour
    {
        private Button _button;
        
        public Button Button
        {
            get
            {
                if (_button == null) 
                    _button = GetComponent<Button>();

                return _button;
            }
        }
    }
}