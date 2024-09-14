using BugStrategy.MiniMap.MiniMapIcons.Configs;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class NeutralUnitMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<NeutralUnitMiniMapIcon, NeutralUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.NeutralUnit;
    }
}