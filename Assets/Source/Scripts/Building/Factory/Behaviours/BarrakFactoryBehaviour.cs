using Zenject;

public class BarrakFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BarrackConfig _barrackConfig;

    public override ConstructionType ConstructionType => ConstructionType.Barrack;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<Barrack> configuration = _barrackConfig.GetConfiguration();

        Barrack construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}

