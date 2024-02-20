using Projectiles;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ProjectilePoolInstaller : MonoInstaller
    {
        [SerializeField] private ProjectilesPool _projectilesPool;
        
        public override void InstallBindings()
        {
            Container.Bind<ProjectilesPool>().FromInstance(_projectilesPool).AsSingle();
        }
    }
}