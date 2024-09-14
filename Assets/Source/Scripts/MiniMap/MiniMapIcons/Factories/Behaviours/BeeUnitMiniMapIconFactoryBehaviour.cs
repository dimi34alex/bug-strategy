using BugStrategy.MiniMap.MiniMapIcons.Configs;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class BeeUnitMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<BeeUnitMiniMapIcon, BeeUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.BeeUnit;
    }
}