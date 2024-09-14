using CycleFramework.Execute;
using Zenject;

namespace BugStrategy.Missions
{
    public class MissionDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var missionIndex = GlobalDataHolder.GlobalData.ActiveMissionIndex;
            var missionConfig = ConfigsRepository.ConfigsRepository.FindConfig<MissionsConfig>().MissionsConfigs[missionIndex];
            
            Container.BindInterfacesAndSelfTo<MissionData>().FromNew().AsSingle().WithArguments(0, missionConfig);
        }
    }
}