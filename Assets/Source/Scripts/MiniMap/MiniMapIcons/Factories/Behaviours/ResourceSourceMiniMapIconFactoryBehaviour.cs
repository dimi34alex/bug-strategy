using BugStrategy.MiniMap.MiniMapIcons.Configs;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class ResourceSourceMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<ResourceSourceMiniMapIcon, ResourceSourceMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.ResourceSource;
    }
}