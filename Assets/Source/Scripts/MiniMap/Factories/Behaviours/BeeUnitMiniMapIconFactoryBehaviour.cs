using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class BeeUnitMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<BeeUnitMiniMapIcon, BeeUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.BeeUnit;
    }
}