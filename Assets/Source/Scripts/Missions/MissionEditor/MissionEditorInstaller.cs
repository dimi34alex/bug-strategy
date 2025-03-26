using BugStrategy.Missions.MissionEditor.GridRepositories;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPosRepository<EditorConstructionsRepository>();
            BindPosRepository<EditorResourceSourcesRepository>();
        }

        private void BindPosRepository<TRep>()
        {
            Container.BindInterfacesAndSelfTo<TRep>().FromNew().AsSingle();
        }
    }
}