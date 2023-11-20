using ConstructionLevelSystem;

public abstract class ResourceConversionConstructionBase<TBuildingLevel> : ResourceProduceConstructionBase<TBuildingLevel>
    where TBuildingLevel : ConstructionLevelBase
{
    public abstract ResourceConversionCore ResourceConversionCore { get; }
    public override ResourceProduceCoreBase ResourceProduceCoreBase => ResourceConversionCore;
}
