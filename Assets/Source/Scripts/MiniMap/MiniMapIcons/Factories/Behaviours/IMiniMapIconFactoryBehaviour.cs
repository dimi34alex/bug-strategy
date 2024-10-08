namespace BugStrategy.MiniMap.MiniMapIcons.Factories
{
    public interface IMiniMapIconFactoryBehaviour
    {
        public MiniMapIconID MiniMapIconID { get; }
        public TMiniMapIcon Create<TMiniMapIcon>() where TMiniMapIcon : MiniMapIconBase;
    }
}