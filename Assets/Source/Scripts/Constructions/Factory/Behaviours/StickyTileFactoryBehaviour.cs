using Constructions;
using Zenject;

public class StickyTileFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeeStickyTileSpawnConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Sticky_Tile_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeeStickyTile> configuration = _config.GetConfiguration();

        BeeStickyTile construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);
        return construction.Cast<TConstruction>();
    }
}
