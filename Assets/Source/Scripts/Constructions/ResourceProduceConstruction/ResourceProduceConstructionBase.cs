
public abstract class ResourceProduceConstructionBase<TBuildingLevel> : EvolvConstruction<TBuildingLevel>
    where TBuildingLevel : BuildingLevelBase
{
    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public abstract ResourceProduceConstructionState ProduceConstructionState { get; }
}