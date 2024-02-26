using UnityEngine;

namespace Projectiles
{
    public class ProjectilesUpdater : CycleInitializerBase
    {
        protected override void OnUpdate()
        {
            var time = Time.deltaTime;
            foreach (var projectiles in FrameworkCommander.GlobalData.ProjectilesRepository.Projectiles)
                foreach (var projectile in projectiles.Value)
                    projectile.HandleUpdate(time);
        }
    }
}