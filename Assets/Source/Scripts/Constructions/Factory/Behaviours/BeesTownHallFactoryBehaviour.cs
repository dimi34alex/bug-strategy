using Constructions;
using Zenject;

public class BeesTownHallFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeeTownHallSpawnConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Bees_Town_Hall;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeeTownHall> configuration = _config.GetConfiguration();

        BeeTownHall construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
