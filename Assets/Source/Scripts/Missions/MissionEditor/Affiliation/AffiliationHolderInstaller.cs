using Zenject;

namespace BugStrategy.Missions.MissionEditor.Affiliation
{
    public class AffiliationHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AffiliationHolder>().FromNew().AsSingle();
        }
    }
}