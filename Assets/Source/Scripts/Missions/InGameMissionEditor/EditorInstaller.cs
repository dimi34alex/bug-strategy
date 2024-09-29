using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class EditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPosRepository<GroundPositionsRepository>();
            BindPosRepository<EditorConstructionsRepository>();
            BindPosRepository<ResourceSourceRepository>();
        }

        private void BindPosRepository<TRep>()
        {
            Container.BindInterfacesAndSelfTo<TRep>().FromNew().AsSingle();
        }
    }
}