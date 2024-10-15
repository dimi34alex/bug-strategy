using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.MissionEditor
{
    public class MissionEditorBuilderInstaller : MonoInstaller
    {
        [SerializeField] private MissionEditorBuilder builder;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MissionEditorBuilder>().FromInstance(builder).AsSingle();
        }
    }
}