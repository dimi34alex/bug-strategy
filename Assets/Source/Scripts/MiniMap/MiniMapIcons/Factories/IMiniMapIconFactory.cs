namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public interface IMiniMapIconFactory
    {
        public TMiniMapIcon Create<TMiniMapIcon>(MiniMapIconID miniMapIconID) where TMiniMapIcon: MiniMapIconBase;
    }
}