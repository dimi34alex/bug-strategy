using Zenject;

namespace BugStrategy.Selection
{
    public class SelectorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Selector>().FromInstance(FindObjectOfType<Selector>(true)).AsSingle();
        }
    }
}