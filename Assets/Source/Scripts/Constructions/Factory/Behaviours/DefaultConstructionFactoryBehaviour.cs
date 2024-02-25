using Zenject;

public class DefaultConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly DefaultConstructionConfig _defaultConstructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Test_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<DefaultConstruction> configuration = _defaultConstructionConfig.GetConfiguration();

        DefaultConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
