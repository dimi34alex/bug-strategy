using Zenject;

namespace Source.Scripts.Missions
{
    public class MissionDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var missionIndex = GlobalDataHolder.GlobalData.ActiveMissionIndex;
            var missionConfig = ConfigsRepository.FindConfig<MissionsConfig>().MissionsConfigs[missionIndex];
            
            Container.BindInterfacesAndSelfTo<MissionData>().FromNew().AsSingle().WithArguments(0, missionConfig);
        }
    }
}