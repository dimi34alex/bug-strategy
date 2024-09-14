using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class AntUnitMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<AntUnitMiniMapIcon, AntUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.AntUnit;
    }
}