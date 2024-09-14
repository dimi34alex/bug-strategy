using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class ButterflyUnitMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<ButterflyUnitMiniMapIcon, ButterflyUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.ButterflyUnit;
    }
}