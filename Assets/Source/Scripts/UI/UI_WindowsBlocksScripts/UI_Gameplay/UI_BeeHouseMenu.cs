using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BeeHouseMenu : UIScreen
{
    BeeHouse _beeHouse;
    
    public void _CallMenu(GameObject beeHouse)
    {
        _beeHouse = beeHouse.GetComponent<BeeHouse>();
    }
    
    public void _BuildingLVL_Up()
    {
        _beeHouse.NextBuildingLevel();
    }
}
