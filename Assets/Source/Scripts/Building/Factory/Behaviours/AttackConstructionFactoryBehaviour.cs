using Zenject;

public class AttackConstructionFactoryBehaviour : ConstructionFactoryBehaviourBase
{
    [Inject] private readonly AttackConstructionConfig _attackConstructionConfig;

    public override ConstructionType ConstructionType => ConstructionType.Attack_Construction;

    public override TConstruction Create<TConstruction>(ConstructionID constructionID)
    {
        ConstructionConfiguration<AttackConstruction> configuration = _attackConstructionConfig.GetConfiguration(constructionID);

        AttackConstruction construction = Instantiate(configuration.ConstructionPrefab,
            configuration.ConstructionPrefab.transform.position, configuration.Rotation);

        return construction.Cast<TConstruction>();
    }
}
