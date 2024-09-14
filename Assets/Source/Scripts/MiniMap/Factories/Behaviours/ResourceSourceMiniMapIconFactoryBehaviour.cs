using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class ResourceSourceMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<ResourceSourceMiniMapIcon, ResourceSourceMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.ResourceSource;
    }
}