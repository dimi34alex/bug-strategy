using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeStoreHouse : EvolvConstruction<BeeStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.BeeStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new BeeStoreHouseLevelSystem(levelSystem, _healthStorage);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity);     
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Wood, CurrentLevel.WoodCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Wood,CurrentLevel.WoodCapacity);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Leaves, CurrentLevel.LeavesCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Leaves,CurrentLevel.LeavesCapacity);

        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, CurrentLevel.PollenCapacity);
                    
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Bees_Wax, CurrentLevel.WaxCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, CurrentLevel.WaxCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Honey, CurrentLevel.HoneyCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Honey, CurrentLevel.HoneyCapacity);
        
        _updateEvent += OnUpdate;
    }

    void OnUpdate()
    {
        
    }
}