using System.Runtime.InteropServices.ComTypes;
using Constructions;
using Zenject;

public class BeeHouseFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeeHouseConfig _beeHouseConfig;

    public override ConstructionType ConstructionType => ConstructionType.BeeHouse;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeeHouse> configuration = _beeHouseConfig.GetConfiguration();

        BeeHouse construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
