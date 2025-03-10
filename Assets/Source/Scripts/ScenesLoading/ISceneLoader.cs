using System;

namespace BugStrategy.ScenesLoading
{
    public interface ISceneLoader
    {
        public event Action OnLoadingStarted;
        public event Action OnLoadingScreenHided;

        public void HideLoadScreen(bool hideLoadScreenInstantly);
        public void LoadScene(int index, bool showLoadScreenInstantly);
        public void LoadTargetScene();
    }
}