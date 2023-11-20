using ConstructionLevelSystem;

public abstract class ResourceProduceConstructionBase<TBuildingLevel> : EvolvConstruction<TBuildingLevel>
    where TBuildingLevel : ConstructionLevelBase
{
    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public abstract ResourceProduceConstructionState ProduceConstructionState { get; }
}