using Zenject;

public class BeesWaxProduceConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly BeesWaxProduceConstructionConfig _beesWaxProduceConstructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Bees_Wax_Produce_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<BeesWaxProduceConstruction> configuration = _beesWaxProduceConstructionConfig.GetConfiguration();

        BeesWaxProduceConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
