using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BeeHouseMenu : UIScreen
{
    BeeHouse _beeHouse;
    
    public void _CallMenu(ConstructionBase beeHouse)
    {
        _beeHouse = beeHouse.Cast<BeeHouse>();
    }
    
    public void _BuildingLVL_Up()
    {
        _beeHouse.NextBuildingLevel();
    }
}
