using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class NeutralUnitMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<NeutralUnitMiniMapIcon, NeutralUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.NeutralUnit;
    }
}