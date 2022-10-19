using Zenject;

public class TownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly TownHallConfig _townHallConfig;

    public override ConstructionType ConstructionType => ConstructionType.Town_Hall;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<TownHall> configuration = _townHallConfig.GetConfiguration();

        TownHall construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
