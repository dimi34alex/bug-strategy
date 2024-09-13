using System.Linq;
using Zenject;

namespace Source.Scripts.Missions
{
    public class MissionDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (GlobalDataHolder.GlobalData.ActiveMission == null)
            {
                var missionConfig = ConfigsRepository.FindConfig<MissionsConfig>().MissionsConfigs.First();
                var missionData = new MissionData(0, missionConfig);
                GlobalDataHolder.GlobalData.SetActiveMission(missionData);
            }
            
            Container.BindInterfacesAndSelfTo<MissionData>().FromInstance(GlobalDataHolder.GlobalData.ActiveMission)
                .AsSingle();
        }
    }
}