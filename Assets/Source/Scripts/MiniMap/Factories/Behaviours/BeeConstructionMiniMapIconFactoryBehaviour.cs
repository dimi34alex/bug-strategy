using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class BeeConstructionMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<BeeConstructionMiniMapIcon, BeeConstructionMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.BeeConstruction;
    }
}