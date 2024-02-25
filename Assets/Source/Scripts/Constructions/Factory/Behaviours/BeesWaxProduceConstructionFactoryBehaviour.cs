using Constructions;
using Zenject;

public class BeesWaxProduceConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeesWaxProduceConstructionSpawnConfig _beesWaxProduceConstructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Bees_Wax_Produce_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionSpawnConfiguration<BeesWaxProduceConstruction> configuration = _beesWaxProduceConstructionConfig.GetConfiguration();

        BeesWaxProduceConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
