using ConstructionLevelSystem;

public class AntStoreHouse : EvolvConstruction<AntStoreHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.AntStoreHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new AntStoreHouseLevelSystem(levelSystem, ref _healthStorage);
    }
}