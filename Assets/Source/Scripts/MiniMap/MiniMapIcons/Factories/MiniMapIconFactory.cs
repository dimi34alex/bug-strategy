using BugStrategy.Factories;
using BugStrategy.MiniMap.MiniMapIcons.Configs;
using Zenject;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class MiniMapIconFactory : FactoryWithId<MiniMapIconID, MiniMapIconBase>
    {
        public MiniMapIconFactory(DiContainer diContainer, MiniMapIconsConfig config) : 
            base(diContainer, config, "MiniMapIcons") { }
    }
}