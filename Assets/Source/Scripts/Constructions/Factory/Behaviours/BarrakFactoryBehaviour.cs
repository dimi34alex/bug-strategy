using Constructions;
using Zenject;

public class BarrakFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeeBarrackSpawnConfig _barrackConfig;

    public override ConstructionType ConstructionType => ConstructionType.Barrack;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeeBarrack> configuration = _barrackConfig.GetConfiguration();

        BeeBarrack construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}

