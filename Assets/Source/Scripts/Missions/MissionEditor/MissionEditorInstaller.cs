using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.Missions.MissionEditor.Grids;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPosRepository<EditorConstructionsGrid>();
            BindPosRepository<EditorResourceSourcesRepository>();
        }

        private void BindPosRepository<TRep>()
        {
            Container.BindInterfacesAndSelfTo<TRep>().FromNew().AsSingle();
        }
    }
}