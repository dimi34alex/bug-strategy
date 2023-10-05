using MiniMapSystem;

public interface IMiniMapIconFactory
{
    public TMiniMapIcon Create<TMiniMapIcon>(MiniMapIconID miniMapIconID) where TMiniMapIcon: MiniMapIconBase;
}