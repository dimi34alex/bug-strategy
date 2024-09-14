using BugStrategy.MiniMap.MiniMapIcons.Configs;

namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public class AntUnitMiniMapIconFactoryBehaviour : MiniMapIconFactoryBehaviourBase<AntUnitMiniMapIcon, AntUnitMiniMapIconConfig>
    {
        public override MiniMapIconID MiniMapIconID => MiniMapIconID.AntUnit;
    }
}