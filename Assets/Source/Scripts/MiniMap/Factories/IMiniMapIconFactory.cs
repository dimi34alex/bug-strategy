using BugStrategy.MiniMap.MiniMapIcons;

namespace BugStrategy.MiniMap.Factories
{
    public interface IMiniMapIconFactory
    {
        public TMiniMapIcon Create<TMiniMapIcon>(MiniMapIconID miniMapIconID) where TMiniMapIcon: MiniMapIconBase;
    }
}