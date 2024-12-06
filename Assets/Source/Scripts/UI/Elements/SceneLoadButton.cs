using BugStrategy.ScenesLoading;
using UnityEngine;
using Zenject;

namespace BugStrategy.UI.Elements
{
    public class SceneLoadButton : ButtonProvider
    {
        [SerializeField] private int sceneIndex;
        [SerializeField] private bool showLoadScreenInstantly;
        
        [Inject] private ISceneLoader _sceneLoader;
        
        private void Awake()
        {
            Button.onClick.AddListener(LoadScene);
        }

        private void LoadScene() 
            => _sceneLoader.LoadScene(sceneIndex, showLoadScreenInstantly);
    }
}