using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace BugStrategy.ScenesLoading
{
    public class SceneLoader : ISceneLoader
    {
        private static int BootstrapSceneIndex => ScenesConfig.BootstrapSceneIndex;
        private static int LoadingSceneIndex => ScenesConfig.LoadingSceneIndex;
        
        private readonly ILoadingScreen _loadingScreen;
        private int _targetSceneIndex = -1;
        
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        [Inject]
        public SceneLoader(ILoadingScreen loadingScreen)
        {
            _loadingScreen = loadingScreen;
            _loadingScreen.OnHided += () => OnLoadingScreenHided?.Invoke();
        }
        
        public void Initialize(bool endInstantly)
        {
            var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if(activeSceneIndex == BootstrapSceneIndex)
                return;
            
            if (_targetSceneIndex <= -1 || activeSceneIndex == _targetSceneIndex)
                EndLoading(endInstantly);

            if (activeSceneIndex == LoadingSceneIndex)
                StartLoadTargetScene();
        }
        
        public void LoadScene(int index, bool showInstantly = false)
        {
            _targetSceneIndex = index;
            
            OnLoadingStarted?.Invoke();
            
            _loadingScreen.Show(showInstantly, () => SceneManager.LoadSceneAsync(LoadingSceneIndex));
        }

        private void EndLoading(bool endInstantly) 
            => _loadingScreen.Hide(endInstantly);

        private void StartLoadTargetScene() 
            => SceneManager.LoadSceneAsync(_targetSceneIndex);
    }
}