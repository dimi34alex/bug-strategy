using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles.WarFog
{
    public class WarFogVisibilityModificatorInstaller : MonoInstaller
    {
        [SerializeField] private bool isVisible;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TileFogVisibilityModificator>().FromNew().AsSingle().WithArguments(isVisible);
        }
    }
}