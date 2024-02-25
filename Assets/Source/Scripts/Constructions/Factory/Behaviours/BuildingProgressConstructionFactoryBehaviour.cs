using Zenject;

public class BuildingProgressConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BuildingProgressConstructionConfig _constructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Building_Progress_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BuildingProgressConstruction> configuration = _constructionConfig.GetConfiguration();

        BuildingProgressConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
 