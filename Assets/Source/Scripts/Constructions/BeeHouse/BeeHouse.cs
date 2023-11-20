using ConstructionLevelSystem;

public class BeeHouse : EvolvConstruction<BeeHouseLevel>
{
    public override ConstructionID ConstructionID => ConstructionID.BeeHouse;

    protected override void OnAwake()
    {
        base.OnAwake();

        levelSystem = new BeeHouseLevelSystem(levelSystem, ref _healthStorage);
    }
}
