using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class AntConstructionMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<AntConstructionMiniMapIcon, AntConstructionMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.AntConstruction;
    }
}