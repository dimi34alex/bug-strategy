using BugStrategy.Missions;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Projectiles
{
    public class ProjectilesUpdater : CycleInitializerBase
    {
        [Inject] private MissionData _missionData;
        
        protected override void OnUpdate()
        {
            var time = Time.deltaTime;
            foreach (var projectiles in _missionData.ProjectilesRepository.Projectiles)
                foreach (var projectile in projectiles.Value)
                    projectile.HandleUpdate(time);
        }
    }
}