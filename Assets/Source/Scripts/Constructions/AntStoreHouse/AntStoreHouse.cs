using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStoreHouse : EvolvConstruction<AntStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.AntStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new AntStoreHouseLevelSystem(levelSystem, _healthStorage);
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Housing, CurrentLevel.HousingCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Housing,CurrentLevel.HousingCapacity);     
        
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Pollen, CurrentLevel.PollenCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, CurrentLevel.PollenCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Wood, CurrentLevel.WoodCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Wood, CurrentLevel.WoodCapacity);
                
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Leaves, CurrentLevel.LeavesCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Leaves, CurrentLevel.LeavesCapacity);
                    
        ResourceGlobalStorage.ChangeCapacity(ResourceID.Ants_Sand, CurrentLevel.SandCapacity);
            
        ResourceGlobalStorage.ChangeValue(ResourceID.Ants_Sand, CurrentLevel.SandCapacity);
        
        
        _updateEvent += OnUpdate;
    }

    void OnUpdate()
    {
        
    }
}