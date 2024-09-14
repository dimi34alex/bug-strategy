using BugStrategy.MiniMap.MiniMapIconConfigs;
using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public class NeutralConstructionMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<NeutralConstructionMiniMapIcon, NeutralConstructionMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.NeutralConstruction;
    }
}