using ConstructionLevelSystem;

public class ButterflyStoreHouse : EvolvConstruction<ButterflyStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.ButterflyStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new ButterflyStoreHouseLevelSystem(levelSystem, ref _healthStorage);
    }
}