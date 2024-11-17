using Zenject;

namespace BugStrategy.CustomInput
{
    public class InputProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputProvider>().FromNew().AsSingle();
        }
    }
}