using ConstructionLevelSystem;

public class BeeStoreHouse : EvolvConstruction<BeeStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.BeeStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new BeeStoreHouseLevelSystem(levelSystem, ref _healthStorage);
    }
}