using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Screens
{
    public class TutorialWindow : MonoBehaviour
    {
        [SerializeField] private Button openButton;
        [SerializeField] private Button closeButton;
        [SerializeField] private GameObject tutorialWindow;
        
        private void Awake()
        {
            openButton.onClick.AddListener(Open);
            closeButton.onClick.AddListener(Close);
        }

        private void Open() 
            => tutorialWindow.SetActive(true);

        private void Close() 
            => tutorialWindow.SetActive(false);
    }
}