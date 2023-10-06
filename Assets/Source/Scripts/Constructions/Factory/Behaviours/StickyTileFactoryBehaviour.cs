using Zenject;

public class StickyTileFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly StickyTileConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Sticky_Tile_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<StickyTile> configuration = _config.GetConfiguration();

        StickyTile construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);
        return construction.Cast<TConstruction>();
    }
}
