using BugStrategy.MiniMap.MiniMapIcons.Configs;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class NeutralConstructionMiniMapIconFactoryBehaviour: MiniMapIconFactoryBehaviourBase<NeutralConstructionMiniMapIcon, NeutralConstructionMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.NeutralConstruction;
    }
}