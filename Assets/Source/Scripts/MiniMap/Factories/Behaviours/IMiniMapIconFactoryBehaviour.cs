using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public interface IMiniMapIconFactoryBehaviour
    {
        public MiniMapIconID MiniMapIconID { get; }
        public TMiniMapIcon Create<TMiniMapIcon>() where TMiniMapIcon : MiniMapIconBase;
    }
}