using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class ButterflyConstructionMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<ButterflyConstructionMiniMapIcon, ButterflyConstructionMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.ButterflyConstruction;
    }
}