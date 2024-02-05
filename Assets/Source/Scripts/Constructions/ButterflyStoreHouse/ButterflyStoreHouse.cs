using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyStoreHouse : EvolvConstruction<ButterflyStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.ButterflyStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new ButterflyStoreHouseLevelSystem(levelSystem, _healthStorage);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity);     
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, CurrentLevel.PollenCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Wood, CurrentLevel.WoodCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Wood, CurrentLevel.WoodCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Leaves, CurrentLevel.LeavesCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Leaves, CurrentLevel.LeavesCapacity);
                    
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Butterfly_Silk, CurrentLevel.SilkCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Butterfly_Silk, CurrentLevel.SilkCapacity);
        
        
        _updateEvent += OnUpdate;
    }

    void OnUpdate()
    {
        
    }
}