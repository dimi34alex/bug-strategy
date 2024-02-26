using Constructions;
using Zenject;

public class StickyTileFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeeStickyTileSpawnConfig _config;

    public override ConstructionType ConstructionType => ConstructionType.Bee_Sticky_Tile_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeeStickyTile> configuration = _config.GetConfiguration();

        TConstruction construction = DiContainer.InstantiatePrefab(configuration.ConstructionPrefab,
                configuration.ConstructionPrefab.transform.position, configuration.Rotation, null)
            .GetComponent<TConstruction>();
        
        return construction;
    }
}
