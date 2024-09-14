using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.UI.Elements
{
    [RequireComponent(typeof(Button))]
    public class SetScreenButton : MonoBehaviour
    {
        [SerializeField] private UIScreenType screenType;
        
        [Inject] private UIController _uiController;
        
        private Button _screenButton;

        private void Awake()
        {
            _screenButton = gameObject.GetComponent<Button>();
            _screenButton.onClick.AddListener(SetScreen);
        }
        
        private void OnDestroy() 
            => _screenButton.onClick.RemoveListener(SetScreen);

        private void SetScreen() 
            => _uiController.SetScreen(screenType);
    }
}
