using Constructions;

public class UI_BeeHouseMenu : UI_EvolveConstructionScreenBase<BeeHouse>
{
    public void _CallMenu(ConstructionBase beeHouse)
    {
        _construction = beeHouse.Cast<BeeHouse>();
    }
}
