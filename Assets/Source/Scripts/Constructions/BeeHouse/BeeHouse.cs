using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHouse : EvolvConstruction<BeeHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.BeeHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new BeeHouseLevelSystem(levelSystem, _healthStorage);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing,CurrentLevel.HousingCapacity);
        
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity);
        
        _updateEvent += OnUpdate;
    }

    void OnUpdate()
    {
        
    }
}
